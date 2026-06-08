Travel Planner

Travel Planner je web aplikacija za planiranje i organizaciju putovanja.
Korisnicima omogućava kreiranje planova putovanja, upravljanje destinacijama i aktivnostima, praćenje troškova i budžeta, vođenje checkliste, kao i dijeljenje planova putovanja pomoću QR koda.

Korišćene tehnologije : 
	Frontend - React,TypeScript,Tailwind CSS,Axios,React Router
	Backend - ASP.NET Core, Entity Framework Core, JWT Authentication, Service Fabric
	Baza podataka - SQL Server

Arhitektura sistema

Sistem je organizovan kao mikroservisna arhitektura.
Gateway – centralna ulazna tačka sistema koja prima zahtjeve sa frontend aplikacije.
ValidatorService – vrši poslovne validacije i koordinaciju između mikroservisa.
UserService – registracija korisnika, prijava, JWT autentifikacija i autorizacija.
TripService – upravljanje planovima putovanja, destinacijama, aktivnostima, checklist stavkama i dijeljenjem planova.
ExpenseService – upravljanje troškovima, budžetom i upozorenjima o potrošnji.

Funkcionalnosti sistema

Autentifikacija korisnika - Korisnik moze registrovati nalog,prijaviti se na sistem,koristiti JWT autentifikaciju

Upravljanje planovima putovanja - Korisnik moze kreirati plan putoavanja,urediti ga,obrisati ga,pogledati sva svoja putovanja

Destinacije - Korisnik moze dodavati destinacije,uredjivati ih i brisati

Aktivnosti- Korisnik može dodavati aktivnosti,uredjivati ih, brisati i pregledati aktivnosti po datumu

Checklist- Korisnik može dodavati checklist stavke,označiti stavku kao završenu i brisati checklist stavke

Troškovi i budžet - Korisnik može dodavati troškove ,uredjivati ih , brisati, pratiti preostali budžet i dobiti upozorenje kada se približi limitu budžeta

Dijeljenje plana putovanja
Sistem omogućava dijeljenje plana putovanja pomoću QR koda.

Validacije
Sistem provjerava:

- krajnji datum ne može biti prije početnog
- budžet ne može biti negativan
- aktivnosti moraju biti unutar trajanja putovanja
- destinacije moraju biti unutar trajanja putovanja
- troškovi moraju biti unutar trajanja putovanja
- obavezna polja ne mogu biti prazna

Baza podataka
Korišćene su:
	Entity Framework Core migracije
	SQL Server baza
Implementirano je cascade delete ponašanje

REST konvencije
Aplikacija koristi REST principe:
	GET → dohvat podataka
	POST → kreiranje
	PUT → izmjena
	DELETE → brisanje

Pokretanje projekta
	Potrebno : .NET 8 , Node.js , SQL Server , Visual Studio , Service Fabric Runtime

Pokretanje backend sistema:
1. Pokrenuti SQL Server.
2. Primijeniti EF Core migracije.
3. Otvoriti rješenje u Visual Studio.
4. Postaviti WebProject kao startup projekat.
5. Pokrenuti aplikaciju preko Service Fabric Local Cluster-a.

Pokretanje frontend aplikacije
Instalacija paketa:
	npm install
Pokretanje aplikacije:
	npm run dev

Mikroservisi

UserService
- Registracija korisnika
- Prijava korisnika
- JWT autentifikacija

TripService
- Planovi putovanja
- Destinacije
- Aktivnosti
- Checklist
- Dijeljenje planova
- QR kodovi

ExpenseService
- Troškovi
- Budžet
- Praćenje potrošnje
- Upozorenja o budžetu

ValidatorService
- Poslovna pravila
- Validacija zahtjeva
- Komunikacija između servisa