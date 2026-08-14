# FoodFrenzy — Database Entity Design

## 1. Purpose

Define the initial database entity structure for the FoodFrenzy platform before implementing the corresponding Domain entities.

The design must support:

- Multiple countries
- Multiple regions
- Multiple cities
- Multiple service areas
- Multiple restaurants
- Multiple restaurant branches
- Multiple menus
- Multiple menu categories
- Multiple menu items

The design must also support future scalability, security, performance and data isolation requirements.

---

## 2. Core Entity Structure

The initial entity structure is:

```text
Country
   ↓
Region
   ↓
City
   ↓
ServiceArea
   ↓
Restaurant
   ↓
RestaurantBranch
   ↓
Menu
   ↓
MenuCategory
   ↓
MenuItem

## 3. Country Entity

Purpose

Represents a country supported by FoodFrenzy.

Initial Fields
Country
├── Id
├── Name
├── Code
├── CurrencyCode
├── IsActive
├── CreatedAt
├── UpdatedAt
└── IsDeleted
Relationships
Country
   ↓
One-to-Many
   ↓
Region
Rules
Country Code must be unique.
Only active countries should be available for normal platform operations.
Country-specific configuration must not be hard-coded into application logic.

## 4. Region Entity

Purpose

Represents a state, province or equivalent administrative region.

Initial Fields
Region
├── Id
├── CountryId
├── Name
├── Code
├── IsActive
├── CreatedAt
├── UpdatedAt
└── IsDeleted
Relationships
Country
   ↓
One-to-Many
   ↓
Region
Rules
A Region belongs to exactly one Country.
Region Code should be unique within its Country.
A Region cannot exist without a valid Country.

## 5. City Entity

Purpose

Represents a city where FoodFrenzy operates.

Initial Fields
City
├── Id
├── RegionId
├── Name
├── Code
├── TimeZone
├── IsActive
├── CreatedAt
├── UpdatedAt
└── IsDeleted
Relationships
Region
   ↓
One-to-Many
   ↓
City
Rules
A City belongs to exactly one Region.
City Code should be unique within its Region.
City time-zone information must be configurable.

## 6. ServiceArea Entity

Purpose

Represents an operational delivery area within a city.

Initial Fields
ServiceArea
├── Id
├── CityId
├── Name
├── Code
├── IsActive
├── CreatedAt
├── UpdatedAt
└── IsDeleted
Relationships
City
   ↓
One-to-Many
   ↓
ServiceArea
Rules
A ServiceArea belongs to exactly one City.
ServiceArea Code should be unique within its City.
A ServiceArea can be activated or deactivated independently.

## 7. Restaurant Entity

Purpose

Represents the restaurant business or restaurant brand.

Initial Fields
Restaurant
├── Id
├── Name
├── Description
├── ContactEmail
├── ContactPhone
├── IsActive
├── CreatedAt
├── UpdatedAt
└── IsDeleted
Relationships
Restaurant
   ↓
One-to-Many
   ↓
RestaurantBranch
Rules
A Restaurant can have multiple branches.
Restaurant-level information belongs to the Restaurant entity.
Branch-specific information must not be stored directly in Restaurant.

## 8. RestaurantBranch Entity

Purpose

Represents a physical operating location of a Restaurant.

Initial Fields
RestaurantBranch
├── Id
├── RestaurantId
├── ServiceAreaId
├── Name
├── AddressLine1
├── AddressLine2
├── PostalCode
├── Latitude
├── Longitude
├── ContactPhone
├── IsActive
├── CreatedAt
├── UpdatedAt
└── IsDeleted
Relationships
Restaurant
   ↓
One-to-Many
   ↓
RestaurantBranch

ServiceArea
   ↓
One-to-Many
   ↓
RestaurantBranch
Rules
A branch belongs to exactly one Restaurant.
A branch belongs to one ServiceArea.
A branch must have a valid geographic location.
Branch operational information must remain separate from Restaurant-level information.

## 9. Menu Entity

Purpose

Represents a menu associated with a Restaurant Branch.

Initial Fields
Menu
├── Id
├── RestaurantBranchId
├── Name
├── Description
├── IsActive
├── CreatedAt
├── UpdatedAt
└── IsDeleted
Relationships
RestaurantBranch
   ↓
One-to-Many
   ↓
Menu
Rules
A Menu belongs to one RestaurantBranch.
A branch can have multiple menus.
Only active menus should normally be available to customers.

## 10. MenuCategory Entity
Purpose

Groups menu items into logical categories.

Examples:

Pizza
Burgers
Beverages
Desserts
Combos
Initial Fields
MenuCategory
├── Id
├── MenuId
├── Name
├── Description
├── DisplayOrder
├── IsActive
├── CreatedAt
├── UpdatedAt
└── IsDeleted
Relationships
Menu
   ↓
One-to-Many
   ↓
MenuCategory

## 11. MenuItem Entity

Purpose

Represents an individual food or beverage item that customers can order.

Initial Fields
MenuItem
├── Id
├── MenuCategoryId
├── Name
├── Description
├── Price
├── IsAvailable
├── DisplayOrder
├── IsActive
├── CreatedAt
├── UpdatedAt
└── IsDeleted
Relationships
MenuCategory
   ↓
One-to-Many
   ↓
MenuItem
Rules
Price must be greater than or equal to zero.
Menu items must belong to a valid MenuCategory.
Availability and active status are separate concepts.
An unavailable item should not normally be orderable.

## 12. Relationship Summary

Country
   │
   └── Region
          │
          └── City
                 │
                 └── ServiceArea
                        │
                        └── RestaurantBranch
                               │
                               ├── Menu
                               │     │
                               │     └── MenuCategory
                               │             │
                               │             └── MenuItem
                               │
                               └── Restaurant


                               
 Our Current Entity Map

We currently have:

Country
   │
   └── Region
         │
         └── City
               │
               └── ServiceArea
                     │
                     └── Restaurant
                           │
                           └── RestaurantBranch
                                 │
                                 └── Menu
                                       │
                                       └── MenuCategory
                                             │
                                             └── MenuItem


                                             Country
                                             
   │ 1
   └──────── * Region
                  │ 1
                  └──────── * City
                                 │ 1
                                 └──────── * ServiceArea
                                                │ 1
                                                ├──────── * Restaurant
                                                │
                                                └──────── * RestaurantBranch
                                                           │
Restaurant ────────────────────────────────────────────────┘
   │ 1
   └──────── * RestaurantBranch
                  │ 1
                  └──────── * Menu
                                 │ 1
                                 └──────── * MenuCategory
                                                │ 1
                                                └──────── * MenuItem


Restaurant ownership relationship:

Restaurant
   ↓
RestaurantBranch

Geographical relationship:

Country
   ↓
Region
   ↓
City
   ↓
ServiceArea
   ↓
RestaurantBranch

## 13. Primary Key Strategy

The initial design will use a primary key for every entity.

Country.Id
Region.Id
City.Id
ServiceArea.Id
Restaurant.Id
RestaurantBranch.Id
Menu.Id
MenuCategory.Id
MenuItem.Id

The final identifier strategy will be standardized before implementation.

## 14. Foreign Key Strategy

Foreign keys will maintain referential integrity.

Examples:

Region.CountryId
City.RegionId
ServiceArea.CityId
RestaurantBranch.RestaurantId
RestaurantBranch.ServiceAreaId
Menu.RestaurantBranchId
MenuCategory.MenuId
MenuItem.MenuCategoryId

Invalid references must be prevented by database constraints and application validation.

## 15. Audit Fields

The initial entities will use common audit fields:

CreatedAt
UpdatedAt
IsDeleted

Future requirements may add:

CreatedBy
UpdatedBy
DeletedAt
DeletedBy

The final audit strategy will be standardized before implementation.

## 16. Soft Delete Strategy

FoodFrenzy will use a soft-delete approach where appropriate.

Instead of immediately physically deleting important business records:

IsDeleted = true

will allow the system to preserve historical information.

Soft-delete behavior must be applied consistently through the application/data-access layer.

## 17. Active Status

Business entities will use an active status where operational activation/deactivation is required.

Example:

IsActive = true

An entity may therefore be:

Active
Inactive
Deleted

These states must be handled consistently by application business rules.

## 18. Index Strategy

Indexes will be added based on actual query requirements.

Initial candidates include:

Country.Code
Region.CountryId + Region.Code
City.RegionId + City.Code
ServiceArea.CityId + ServiceArea.Code
RestaurantBranch.RestaurantId
RestaurantBranch.ServiceAreaId
Menu.RestaurantBranchId
MenuCategory.MenuId
MenuItem.MenuCategoryId

Indexes will be reviewed during database implementation and performance testing.

## 19. Data Isolation Direction

FoodFrenzy must enforce authorized data access.

Restaurant-related data must be scoped correctly so that:

Restaurant owners access only their restaurants.
Restaurant managers access only assigned branches.
Restaurant staff access only permitted operational data.
Customers access only their own protected information.
Platform administrators access data according to their assigned permissions.

The detailed tenant and authorization model will be defined separately.

## 20. Future Entity Requirements

The following entities are intentionally not included in this initial design and will be designed in later phases:

CustomerProfile
Address
Role
Permission
UserRole
RestaurantStaff
DeliveryPartner
Cart
CartItem
Order
OrderItem
Payment
Delivery
Notification
Promotion
Review
Rating
TaxConfiguration
CurrencyConfiguration
OperatingHours
RestaurantServiceArea

These will be introduced after the core geography and restaurant structure is finalized.

## 21. Current Status

Status: Database Design Defined

The initial geography, restaurant and menu entity structure has been defined.

No new Domain entities have been implemented from this document yet.

Entity Framework Core configurations will be maintained in the Infrastructure layer.

Database migrations will be created only after the entity implementation and configuration have been reviewed and tested.