# Proyecto Fullstack - MillionManagerApp

Aplicación Fullstack para gestión de propiedades (prueba técnica).  
Incluye **backend en .NET 8 + MongoDB** y **frontend en React + Vite + TailwindCSS**.  

---

## 🚀 Tecnologías y Herramientas utilizadas

- C# - .Net Core - Framework .Net9
- VisualStudio 2022
- REST API (simulada o real)
- React 19.1.1
- Bootstrap 5.3.7
---

## 🛠️ Características del sistema

- Inicio en plataforma Swagger
- Ingreso de datos formato Json
- Sistema valida los datos, consult5ando la base de datos de MongoDB.
- Se recuperan datos y se muetsra en pantalla.

---

## 📂 Estructura del proyecto

/PropertyManagerApp
	|- backend/ # API .NET (Clean Architecture)
		|- src/
			|- Core/ # Domain & Application
			|- Infrastructure/ # Repositories + Mongo Context
			|- Web/ # API Web (Controllers, Swagger)
	|- frontend/ # Cliente en React
		|- million-manager-app/
			|- src/ # Componentes React
	|- backup/ # Backup MongoDB 
	|- README.md

 ---

 ***

Enlaces

Swagger:    http://localhost:8081/api/swagger-ui/index.html
Base Datos: http://localhost:8081/h2-console/login.jsp

---

## ⚙️ Backend (.NET 8 + MongoDB)

### 📌 Requisitos previos
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [MongoDB](https://www.mongodb.com/try/download/community)

---

### Pasos:

1. Clonar el repositorio:
   ```bash
   git clone https://github.com/edwsilme/PropertyManagerApp.git

2. Cambiar a Rama develop:
   ```bash
   git switch develop

3. Descomprimir archivo BAckup.rar

4. Restaurar el backup de MongoDB:
   ```bash
   mongorestore --db PropertyStateDb ./backup/PropertyStateDb

5. Dirijirse a la carpeta WebApi
   ```bash
   cd backend/src/Web/MillionManagerApp.Web

6. Ejecutar la API:
   ```bash
   dotnet run --launch-profile https

## Frontend (React + Vite + Tailwind)

1. Dirijirse a la carpeta ManagerApi:
   ```bash
   cd frontend/million-manager-app

2. Instalar dependencias:
   ```bash
   npm install

3. Ejecutar desarrollo:
   ```bash
   npm run dev

---
Enlaces:

Api Swagger: https://localhost:7113/swagger
Web: http://localhost:5173

---

### Screenshot

:arrow_forward: Pantalla de inicio de la aplicación Swagger:<p>
<img src="https://github.com/edwsilme/raw/blob/main/img-prueba-million/001.png" width="500">

:arrow_forward: Ejecución en Swagger:<p>
<img src="https://github.com/edwsilme/raw/blob/main/img-prueba-million/004.png" width="500">

:arrow_forward: Aplicación Web:<p>
<img src="https://github.com/edwsilme/raw/blob/main/img-prueba-million/002.png" width="500">

:arrow_forward: Detalles:<p>
<img src="https://github.com/edwsilme/raw/blob/main/img-prueba-million/003.png" width="500">

:arrow_forward: Vista Collections MongoDB:<p>
<img src="https://github.com/edwsilme/raw/blob/main/img-prueba-million/005.png" width="500">





