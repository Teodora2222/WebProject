Travel Planner

Travel Planner je web aplikacija za planiranje i organizaciju putovanja.
Korisnicima omogućava kreiranje planova putovanja, upravljanje destinacijama i aktivnostima, praćenje troškova i budžeta, vođenje checkliste, kao i dijeljenje planova putovanja pomoću QR koda.

Korišćene tehnologije : 
	Frontend - React,TypeScript,Tailwind CSS,Axios,React Router
	Backend - ASP.NET Core Web API,Entity Framework Core,JWT autentifikacija,Service Fabric
	Baza podataka - SQL Server

Arhitektura sistema

Sistem je organizovan kao mikroservisna arhitektura.
UserService zadužen za: registraciju korisnika,prijavu korisnika,generisanje JWT tokena,autentifikaciju i autorizaciju
TripService zadužen za: planove putovanja,destinacije,aktivnosti,checklist stavke,dijeljenje planova putovanja,QR kod pristup
ExpenseService zadužen za: troškove,budžet,praćenje potrošnje,smart budget warning

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
	krajnji datum ne može biti prije početnog,budžet ne može biti negativ,anaktivnosti moraju biti unutar trajanja putovanja,destinacije moraju biti unutar trajanja putovanja,obavezna polja ne mogu biti prazna

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

Pokretanje backend servisa
Za svaki servis:
	Pokretanje migracija  -  Update-Database
	Pokretanje servisa -  dotnet run (bez fabric pokrece se svaki servis posebno sa fabric radi se publish)

Pokretanje frontend aplikacije
Instalacija paketa:
	npm install
Pokretanje aplikacije:
	npm run dev