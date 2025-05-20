import React, { useState } from 'react';
import './App.css';

function App() {
  const [messages, setMessages] = useState([]); // mesaj listesi
  const [input, setInput] = useState("");       // input kutusu

  const handleSend = async () => {
    if (!input.trim()) return;

    // Mesajı listeye ekle
    const newMessages = [...messages, { sender: 'user', text: input }];
    setMessages(newMessages);

    try {
      const response = await fetch("http://localhost:3000/chat", {
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify({ message: input })
      });

      const data = await response.json();
      const reply = data.result; // artık dizi olarak işlenecek

      setMessages(prev => [...prev, { sender: 'bot', text: reply }]);
    } catch (error) {
      setMessages(prev => [...prev, { sender: 'bot', text: "❌ Hata: " + error.message }]);
    }

    setInput(""); // input temizle
  };

  return (
    <div className="App">
      <h1>✈️ Uçuş Asistanı</h1>
      <div className="chat-window">
        {messages.map((msg, i) => (
  <div key={i} className={`message ${msg.sender}`}>
    <b>{msg.sender === 'user' ? 'Sen' : 'Bot'}:</b>

    {/* Eğer bot mesajı bir JSON dizisiyse kart gibi göster */}
   {msg.sender === 'bot' && Array.isArray(msg.text) ? (
  msg.text.length > 0 ? (
    msg.text.map((flight, idx) => (
      <div key={idx} className="flight-card">
        <div><b>Uçuş ID:</b> {flight.id}</div>
        <div><b>Kalkış:</b> {flight.airportFrom} → {flight.airportTo}</div>
        <div><b>Tarih:</b> {flight.dateFrom}</div>
        <div><b>Süre:</b> {flight.duration} dakika</div>
        <div><b>Kapasite:</b> {flight.capacity}</div>
        <div><b>Boş Koltuk:</b> {flight.availableSeats}</div>
      </div>
    ))
  ) : (
    <div>❗ Uygun uçuş bulunamadı.</div>
  )
) : (
  <div>{typeof msg.text === "string" ? msg.text : JSON.stringify(msg.text)}</div>
)}

  </div>
))}

      </div>
      <div className="input-area">
        <input
          type="text"
          value={input}
          placeholder="Mesaj yaz..."
          onChange={(e) => setInput(e.target.value)}
          onKeyDown={(e) => e.key === "Enter" && handleSend()}
        />
        <button onClick={handleSend}>Gönder</button>
      </div>
    </div>
  );
}

export default App;
