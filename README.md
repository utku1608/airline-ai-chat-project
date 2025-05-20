# ✈️ SE4458 - Airline AI Chat Assistant Project

This project was developed for **SE4458 – Software Architecture & Design of Large Scale Systems**, Spring 2025.  
It extends a RESTful airline ticketing API with an AI-powered chat interface that uses OpenAI GPT for intent recognition and natural language understanding.

---

## 🔗 Project Links

- 🔗 **GitHub Repository:**  
  https://github.com/utku1608/airline-ai-chat-final

- 🎥 **Demo Video (YouTube or Google Drive):**  
  https://drive.google.com/file/d/1yUX6OY5KZTYVzwfvp0lqcjY8WzcxTUJv/view?usp=sharing

---

## 🚀 Technologies Used

- .NET 8 (ASP.NET Core Web API)  
- Entity Framework Core (SQLite)  
- React (Chat UI)  
- Node.js + Express (API Gateway)  
- OpenAI GPT-3.5 API  
- JWT Authentication  
- Swagger for API testing  
- Git + GitHub  

---

## ✅ Features

- Add new flights via Swagger  
- Query available flights with paging support  
- Buy tickets using chat or Swagger  
- Check-in passengers via chat  
- List passenger information (paging supported)  
- JWT-based authentication for protected endpoints  
- Natural language flight booking (e.g., “Buy ticket for 2nd flight”)  
- Chat UI built in React  
- OpenAI-powered intent and parameter parsing  
- Smart chat handling via API Gateway  

---

## 🧱 System Design

[React Chat UI] → [Node.js Gateway] → [OpenAI GPT API]
↓
[ASP.NET Core REST API] → [SQLite DB]

---

## 🧠 Assumptions

- System supports only one-way flights  
- Seat numbers are assigned like P1, P2, ...  
- Only one static user: `admin / 1234`  
- Data is stored in SQLite DB (previously in-memory)  
- OpenAI model is instructed to return structured JSON  
- `selectedIndex` is used for “2nd flight” intent  

---

## ❗ Issues Encountered

- OpenAI sometimes omits necessary parameters → solved with Gateway auto-fallback  
- GitHub push protection blocked secrets → `.env` ignored and repo cleaned  
- EF Core `.Date` filtering required `.AsEnumerable()` to support time-agnostic matching  
- Managing state across flight queries and ticket purchase in Gateway  
- Mapping `selectedIndex` from user message to actual flight in memory  

---

## 📸 Sample Chat Flow

**User:** 5 Mayıs’ta İstanbul’dan Ankara’ya 2 kişilik uçuş var mı?  
**Bot:** 3 flights found (Uçuş ID, Tarih, vs.)

**User:** 2. uçuş için bilet al  
**Bot:** 🎫 TICKET PURCHASED! Your ticket number is: ABC123

**User:** satın alınan uçuş için "name surname " adına Check-in yap  
**Bot:** ✅ Checked in! Seat assigned: P1  

---

## 👨‍💻 Developer Info

**Utku Derici**  
Group 1 – SE4458  
Spring 2025 – Airline AI Agent Project
