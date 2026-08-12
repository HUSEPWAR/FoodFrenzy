# FoodFrenzy — Global Geography and Restaurant Architecture

## 1. Purpose

Define the geographical and restaurant structure required for FoodFrenzy to operate across multiple countries and cities.

---

## 2. Global Geography Hierarchy

FoodFrenzy will use the following hierarchy:

Country
   ↓
Region / State / Province
   ↓
City
   ↓
Service Area
   ↓
Restaurant
   ↓
Restaurant Branch
   ↓
Menu
   ↓
Menu Category
   ↓
Menu Item
---

## 3. Country

A Country represents a supported country in FoodFrenzy.

Country-level configuration may include:

- Currency
- Time zone
- Language
- Address format
- Tax configuration
- Payment configuration
- Regulatory configuration

The system must not be hard-coded for a single country.

---

## 4. Region

A Region represents a state, province or equivalent administrative area within a country.

Examples:

India
- Maharashtra
- Karnataka
- Telangana

USA
- California
- Texas
- New York

The system will use a generic Region concept so that different countries can be supported.

---

## 5. City

A City belongs to a Region.

Examples:

Maharashtra
- Mumbai
- Pune
- Nagpur
- Nashik

The same architecture must support cities in every supported country.

---

## 6. Service Area

A Service Area represents the operational delivery area within a city.

Example:

Pune
- Baner
- Wakad
- Hinjawadi
- Kothrud
- Viman Nagar

A restaurant may operate in one or more service areas depending on its delivery capability.

---

## 7. Restaurant

A Restaurant represents the food business or restaurant brand.

A restaurant may have multiple branches.

Example:

Restaurant
    ↓
Domino's
    ↓
Multiple Branches

The Restaurant entity represents the business-level information.

---

## 8. Restaurant Branch

A Restaurant Branch represents a physical operating location of a restaurant.

A branch belongs to:

- One Restaurant
- One City
- One Service Area

A branch may have its own:

- Address
- Contact information
- Operating hours
- Availability
- Menu availability
- Order operations
- Delivery configuration

---

## 9. Restaurant and Branch Relationship

The relationship will be:

Restaurant
   ↓
One-to-Many
   ↓
Restaurant Branch

Example:

Domino's
├── Baner Branch
├── Wakad Branch
└── Kothrud Branch

---

## 10. Menu Structure

The menu structure will be:

Restaurant Branch
   ↓
Menu
   ↓
Menu Category
   ↓
Menu Item

Example:

Pizza Branch
   ↓
Menu
   ├── Pizza
   │   ├── Margherita
   │   └── Farmhouse
   │
   └── Beverages
       ├── Coke
       └── Water

---

## 11. User Access Scope

Users will access geographical and restaurant data according to their role and permissions.

Customer
   ↓
Own Account
   ↓
Own Orders

Restaurant Owner
   ↓
Authorized Restaurant
   ↓
Authorized Branches

Restaurant Manager
   ↓
Assigned Branch

Restaurant Staff
   ↓
Assigned Branch

Delivery Partner
   ↓
Assigned Delivery

Platform Administrator
   ↓
Authorized Platform Scope

---

## 12. Data Isolation

FoodFrenzy must ensure that users cannot access data outside their authorized scope.

Examples:

- A restaurant owner cannot manage another restaurant.
- A branch manager cannot manage an unrelated branch.
- Restaurant staff cannot access unauthorized restaurant data.
- Delivery partners can access only required delivery information.
- Customers can access only their own protected customer information and orders.

Authorization will be enforced at the application level.

---

## 13. Core Relationship Direction

The initial relationship direction is:

Country
   ↓
Region
   ↓
City
   ↓
Service Area
   ↓
Restaurant
   ↓
Restaurant Branch
   ↓
Menu
   ↓
Menu Category
   ↓
Menu Item



                         FoodFrenzy
                             │
                    ┌────────┴────────┐
                    │                 │
                 Country            Users
                    │
              Region / State
                    │
                  City
                    │
              Service Area
                    │
               Restaurant
                    │
              Restaurant Branch
                    │
                   Menu
                    │
              Menu Category
                    │
                Menu Item

---

## 14. Order Relationship Direction

The future order flow will connect:

Customer
   ↓
Cart
   ↓
Order
   ├── Restaurant Branch
   ├── Order Items
   ├── Payment
   └── Delivery
          ↓
    Delivery Partner

---

## 15. International Expansion Requirement

The architecture must support adding a new country without redesigning the entire system.

Example:

India
USA
UK
Canada
Australia
UAE
Singapore

These are examples of future supported countries and are not yet implemented.

---

## 16. Current Status

Status: Architecture Defined

The geographical and restaurant hierarchy has been defined.

Database entities and relationships are not yet implemented.

---

## 17. Next Step

The next architecture phase will define:

- Exact entities
- Primary keys
- Foreign keys
- Entity relationships
- Required fields
- Optional fields
- Unique constraints
- Index requirements
- Soft-delete strategy
- Audit fields
- Tenant/data-isolation strategy