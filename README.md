# Snus API

Detta är ett API byggt med .NET 7 och MongoDB, hostat på **Azure App Service**.  
Det används tillsammans med ett **Express API** som körs lokalt.

## **Länk till API:et**
Swagger UI  (https://mlsnusapi.azurewebsites.net/swagger/index.html) – Testa API:t direkt  
Hämta alla snusprodukter  (https://mlsnusapi.azurewebsites.net/snus) – JSON-lista  

---

##  **Hur API:t fungerar**
.NET API:et är kopplat till en **MongoDB-databas** och hanterar **CRUD-operationer**:

| Metod  | Endpoint         | Beskrivning                  |
|--------|-----------------|------------------------------|
| `GET`  | `/snus`         | Hämta alla snusprodukter    |
| `GET`  | `/snus/{id}`    | Hämta en specifik snus      |
| `POST` | `/snus`         | Lägg till en ny snus       |
| `PUT`  | `/snus/{id}`    | Uppdatera en snus          |
| `DELETE` | `/snus/{id}`  | Ta bort en snus            |

---

##  **Express API**
Express API körs **lokalt** och hämtar data från .NET API:t.  

 **Starta Express API:**  
cd express-api
npm install
node server.js
