Country
  │
  └── Region (1 : Many)
        │
        └── City (1 : Many)
              │
              └── ServiceArea (1 : Many)
                    │
                    ├── Restaurant (1 : Many)
                    │       │
                    │       └── RestaurantBranch (1 : Many)
                    │
                    └── RestaurantBranch (1 : Many)

RestaurantBranch
      │
      └── Menu (1 : Many)
             │
             └── MenuCategory (1 : Many)
                    │
                    └── MenuItem (1 : Many)