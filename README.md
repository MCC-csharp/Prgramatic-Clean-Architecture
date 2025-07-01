# Pragmatic Clean Architecture (.NET 8)

Ce projet est le fruit de la formation de Milan Jovanović sur l’architecture logicielle. Il a pour but de mettre en pratique les principes de **Clean Architecture** avec .NET 8, en suivant des patterns tels que : séparation des couches, injection de dépendances, Domain-Driven Design (DDD), etc.

---

## 🚀 Objectifs

- Structurer une application .NET de manière claire, modulaire et testable
- Implémenter la Clean Architecture (Use Cases, Interfaces, Adapters)
- Travailler avec l'injection de dépendances, la validation, les services, etc.

---

## 🧱 Stack technique

- .NET 8
- ASP.NET Core
- C# 12
- MediatR (CQRS)
- FluentValidation
- Entity Framework Core (si utilisé)
- xUnit / NUnit (à venir)

---

## 🗂 Structure du projet

```bash
📦 src/
 ┣ 📂 Application          # Use cases, interfaces
 ┣ 📂 Domain               # Entités métier
 ┣ 📂 Infrastructure       # Accès aux données, services extérieurs
 ┣ 📂 WebAPI               # Présentation (Controllers)
 ┗ 📂 Tests                # Tests unitaires (à venir)
