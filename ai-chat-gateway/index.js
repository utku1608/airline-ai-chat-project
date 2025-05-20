let lastQueriedFlights = [];

require("dotenv").config();
const express = require("express");
const axios = require("axios");
const bodyParser = require("body-parser");
const cors = require("cors");

const app = express();
const PORT = 3000;

app.use(cors());
app.use(bodyParser.json());

app.post("/chat", async (req, res) => {
  const userMessage = req.body.message;

  try {
    const openaiResponse = await axios.post(
      "https://api.openai.com/v1/chat/completions",
      {
        model: "gpt-3.5-turbo",
        messages: [
          {
            role: "system",
           content: `Kullanıcının uçuşla ilgili mesajını analiz et. Aşağıdaki formatta sadece geçerli bir JSON döndür:

{
  "intent": "QueryFlight" | "BuyTicket" | "CheckIn",
  "parameters": {
    // QueryFlight için:
    "dateFrom": "yyyy-mm-dd",
    "dateTo": "yyyy-mm-dd",
    "airportFrom": "IST",
    "airportTo": "ESB",
    "numberOfPeople": 1,

    // BuyTicket için:
    "flightIndex": 2,
    "passengerName": "Utku Derici",

    // CheckIn için (BuyTicket’la aynı mantık: index üzerinden):
    "flightIndex":   1,
    "date":          "yyyy-mm-dd",
   "passengerName": "Utku Derici"
  }
}

Kurallar:
- Eğer kullanıcı "X. uçuşu al" gibi konuşuyorsa, BuyTicket intent'ini seç ve sadece flightIndex gönder.
- QueryFlight için dateFrom ve dateTo her zaman bugünden ileri bir tarih (örneğin, 2025-05-20) olsun.
- **CheckIn intent’inde mutlaka** flightId, date ve passengerName alanlarını ekle.
- Açıklama, metin, başka içerik verme. Sadece geçerli JSON döndür.`

          },
          { role: "user", content: userMessage }
        ],
        temperature: 0.3
      },
      {
        headers: {
          Authorization: `Bearer ${process.env.OPENAI_API_KEY}`,
          "Content-Type": "application/json"
        }
      }
    );

    console.log("👉 OpenAI cevabı:", openaiResponse.data.choices[0].message.content);

    const parsed = JSON.parse(openaiResponse.data.choices[0].message.content);
    const intent = parsed.intent;
    const parameters = parsed.parameters;
 


    const loginRes = await axios.post(`${process.env.ASP_API_BASE_URL}/auth/login`, {
      username: "admin",
      password: "1234"
    });

    const token = loginRes.data.token;

    if (intent === "QueryFlight") {
   // 1) API’den uçuşları al
   const response = await axios.post(
     `${process.env.ASP_API_BASE_URL}/flight/query`,
     parameters
   );
   const flights = response.data;
   lastQueriedFlights = flights;

   // 2) Uçuş yoksa kullanıcıyı bilgilendir
   if (!flights.length) {
     return res.json({ result: "Üzgünüm, uygun uçuş bulunamadı." });
   }

  // 3) Parametrelerden uçuş bilgisini al ve tarihi Türkçe formatla
   const { airportFrom, airportTo, dateFrom, numberOfPeople } = parameters;
   const dateObj = new Date(dateFrom);
   const dateFormatted = dateObj.toLocaleDateString("tr-TR", {
     month: "long",
     day: "numeric",
   });

   // 4) Başlık mesajını hazırla
   let message = `Harika! İşte bulduklarım:\n`;
   message += `Uçuş: ${airportFrom}-->${airportTo}\n`;
   message += `Tarih: ${dateFormatted}\n`;
   message += `Yolcular: ${numberOfPeople}\n\n`;

   // 5) Her uçuşu listele
   flights.forEach((f, i) => {
     const time = new Date(f.dateFrom).toLocaleTimeString("tr-TR", {
       hour: "2-digit",
       minute: "2-digit",
     });
     message += `${i + 1}. uçuş saat ${time} kapasite ${f.capacity} boş koltuk ${f.availableSeats}\n`;
   });

  // 6) Tek bir string olarak dön
   return res.json({ result: message });
 

  //BUYTICKETBUYTICKET BUYTICKET
    } else if (intent === "BuyTicket") {
  try {
    const index = parameters.flightIndex;
    if (!lastQueriedFlights?.length || !lastQueriedFlights[index - 1]) {
      return res
        .status(400)
        .json({ message: "Geçerli bir uçuş listesi veya index bulunamadı." });
    }

    const selectedFlight = lastQueriedFlights[index - 1];

    parameters.flightId = selectedFlight.id;
    parameters.date     = selectedFlight.dateFrom;
    delete parameters.flightIndex;

    console.log("🛫 Seçilen uçuş:", selectedFlight);
    console.log("📦 Gönderilen parameters:", parameters);

    const result = await axios.post(
      `${process.env.ASP_API_BASE_URL}/flight/buy`,
      parameters,
      { headers: { Authorization: `Bearer ${token}` } }
    );

    const { message, ticketNumber } = result.data;
    return res.json({
      result: `🎫 ${message.toUpperCase()}! Your ticket number is: ${ticketNumber}`,
    });
  } catch (error) {
    console.error("❌ BuyTicket hatası:", error?.response?.data || error.message);
    return res
      .status(500)
      .json({ message: "Hata oluştu", details: error?.response?.data });
  }

  //CHECK IN CHECKIN CHECKIN CHECKIN 
      } else if (intent === "CheckIn") {
  try {
    // 1. flightIndex, date, passengerName LLM’den geldi
    const { flightIndex, date, passengerName } = parameters;

    // 2. son sorguda sakladığın uçuş listesinden seç
    if (!lastQueriedFlights?.length || !lastQueriedFlights[flightIndex - 1]) {
      return res
        .status(400)
        .json({ message: "Geçerli bir uçuş listesi veya index bulunamadı." });
    }
    const selectedFlight = lastQueriedFlights[flightIndex - 1];

    // 3. Gerçek payload’u oluştur
    const payload = {
      flightId: selectedFlight.id,
      date:     selectedFlight.dateFrom,     // zamanlı haliyle
      passengerName
    };
    console.log("📥 CheckIn payload:", payload);

    // 4. API çağrısı (authorization gerekirse ekle)
    const result = await axios.post(
      `${process.env.ASP_API_BASE_URL}/flight/checkin`,
      payload
      // , { headers: { Authorization: `Bearer ${token}` } }
    );

    // 5. Gelen cevabı formatla ve dön
    const { message, seatNumber } = result.data;
    const formatted = `✅ ${message.charAt(0).toUpperCase() + message
      .slice(1)}! Seat assigned: ${seatNumber}`;
    return res.json({ result: formatted });
  } catch (error) {
    console.error("❌ CheckIn hatası:", error?.response?.data || error.message);
    return res
      .status(500)
      .json({ message: "Hata oluştu", details: error?.response?.data });
  }




    } else {
      return res.status(400).json({ message: "Bilinmeyen intent" });
        }

  } catch (error) {
    console.error("❌ OpenAI ya da genel hata:", error?.response?.data || error.message);
    return res.status(500).json({ message: "Hata oluştu", details: error?.response?.data });
  }
});

app.listen(PORT, () => {
  console.log(`✅ Gateway çalışıyor: http://localhost:${PORT}`);
});