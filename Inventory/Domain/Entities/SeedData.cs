using Inventory.Services.Abstractions;

namespace Inventory.Domain.Entities
{
    public static class SeedData
    {
        public static void Initialize(IProductService productService, IWarehouseService warehouseService)
        {
            SeedProducts(productService);
            SeedWarehouses(warehouseService);
        }

        private static void SeedProducts(IProductService productService)
        {
            var products = new[]
            {
                new Product("Laptop Dell XPS 13", "High-performance ultrabook with 13-inch display", 1299.99m),
                new Product("Wireless Mouse Logitech MX Master 3", "Ergonomic wireless mouse with advanced features", 99.99m),
                new Product("USB-C Hub Anker 7-in-1", "Multi-port USB-C adapter with HDMI and Ethernet", 49.99m),
                new Product("Monitor LG 27 UltraFine 4K", "27-inch 4K IPS monitor with USB-C connectivity", 699.99m),
                new Product("Mechanical Keyboard Keychron K2", "Wireless mechanical keyboard with RGB backlight", 79.99m),
                new Product("Laptop Stand Aluminum", "Adjustable laptop stand for better ergonomics", 39.99m),
                new Product("Webcam Logitech C920", "Full HD 1080p webcam with auto-focus", 79.99m),
                new Product("Headphones Sony WH-1000XM4", "Wireless noise-canceling over-ear headphones", 349.99m),
                new Product("Portable SSD Samsung T7 1TB", "High-speed external SSD with USB 3.2 Gen 2", 139.99m),
                new Product("Desk Lamp LED with USB Charging", "Adjustable LED desk lamp with built-in USB port", 45.99m),
                new Product("Office Chair Ergonomic", "Adjustable office chair with lumbar support", 299.99m),
                new Product("Router ASUS AX6000", "WiFi 6 dual-band wireless router", 249.99m),
                new Product("External Hard Drive WD 4TB", "USB 3.0 external hard drive for backup", 89.99m),
                new Product("Bluetooth Speaker JBL Flip 5", "Portable waterproof Bluetooth speaker", 119.99m),
                new Product("Power Bank Anker 20000mAh", "High-capacity portable charger with fast charging", 49.99m),
                new Product("Graphics Tablet Wacom Intuos", "Digital drawing tablet for creative professionals", 199.99m),
                new Product("Docking Station CalDigit TS3 Plus", "Thunderbolt 3 dock with 15 ports", 299.99m),
                new Product("Cable Organizer Kit", "Complete set of cable management accessories", 24.99m),
                new Product("Surge Protector Belkin 12-Outlet", "Power strip with surge protection and USB ports", 34.99m),
                new Product("KVM Switch 2-Port", "Share keyboard, mouse, and monitor between 2 PCs", 54.99m),
                new Product("Network Switch Gigabit 8-Port", "Unmanaged Ethernet switch for home/office", 29.99m),
                new Product("Printer HP LaserJet Pro", "Wireless monochrome laser printer", 199.99m),
                new Product("Scanner Fujitsu ScanSnap", "High-speed document scanner with WiFi", 449.99m),
                new Product("Microphone Blue Yeti", "USB condenser microphone for streaming", 129.99m),
                new Product("Ring Light 10-inch", "LED ring light with tripod for video calls", 34.99m),
                new Product("Smart Plug TP-Link 4-Pack", "WiFi-enabled smart plugs with app control", 39.99m),
                new Product("HDMI Cable 10ft Braided", "High-speed HDMI 2.1 cable supports 4K 120Hz", 19.99m),
                new Product("USB Flash Drive 128GB", "High-speed USB 3.1 flash drive", 24.99m),
                new Product("SD Card SanDisk 256GB", "High-capacity microSD card with adapter", 44.99m),
                new Product("Screen Cleaning Kit", "Complete screen cleaning solution for devices", 14.99m),
                new Product("Laptop Backpack Anti-Theft", "Water-resistant backpack with USB charging port", 49.99m),
                new Product("Standing Desk Converter", "Height-adjustable standing desk converter", 159.99m),
                new Product("Monitor Arm Dual Mount", "Adjustable dual monitor mount with VESA support", 89.99m),
                new Product("Keyboard Tray Under Desk", "Ergonomic keyboard tray with mouse platform", 44.99m),
                new Product("Footrest Adjustable", "Ergonomic footrest with angle adjustment", 34.99m),
                new Product("Anti-Fatigue Mat", "Comfort mat for standing desks", 39.99m),
                new Product("Privacy Screen Filter 15.6-inch", "Anti-glare privacy filter for laptop", 39.99m),
                new Product("Wireless Charger 3-in-1", "Charging station for phone, watch, and earbuds", 54.99m),
                new Product("Cable Sleeves Set", "Neoprene cable management sleeves", 19.99m),
                new Product("Desk Organizer Bamboo", "Multi-compartment desk organizer", 29.99m),
                new Product("Whiteboard Dry Erase 24x36", "Magnetic whiteboard with marker tray", 39.99m),
                new Product("Label Maker Bluetooth", "Portable label maker with smartphone app", 79.99m),
                new Product("Paper Shredder Cross-Cut", "Personal document shredder for home office", 69.99m),
                new Product("Calculator Scientific", "Advanced scientific calculator for students", 24.99m),
                new Product("Stapler Heavy Duty", "High-capacity stapler for thick documents", 19.99m),
                new Product("Hole Punch 3-Hole", "Adjustable 3-hole punch up to 30 sheets", 14.99m),
                new Product("Desk Calendar 2026", "Large monthly desk calendar with notes section", 12.99m),
                new Product("Notebook Set Hardcover", "Premium hardcover notebook 5-pack", 29.99m),
                new Product("Pen Set Gel Ink", "Smooth writing gel pens assorted colors 24-pack", 16.99m),
                new Product("Sticky Notes Variety Pack", "Assorted sizes and colors sticky notes", 19.99m)
            };

            foreach (var product in products)
            {
                productService.Add(product);
            }
        }

        private static void SeedWarehouses(IWarehouseService warehouseService)
        {
            var warehouses = new[]
            {
                new Warehouse("Main Distribution Center", "1500 Commerce Blvd, Los Angeles, CA 90001"),
                new Warehouse("East Coast Hub", "2100 Industrial Way, Newark, NJ 07102"),
                new Warehouse("Midwest Regional", "3400 Logistics Dr, Chicago, IL 60601"),
                new Warehouse("Pacific Northwest", "750 Harbor Ave, Seattle, WA 98104"),
                new Warehouse("Southwest Distribution", "5200 Desert Rd, Phoenix, AZ 85003"),
                new Warehouse("Texas Central", "8900 Commerce St, Dallas, TX 75201"),
                new Warehouse("Florida Southeast", "4100 Port Blvd, Miami, FL 33131"),
                new Warehouse("New England Center", "1200 Bay State Rd, Boston, MA 02115"),
                new Warehouse("Mountain States", "6700 Altitude Way, Denver, CO 80202"),
                new Warehouse("Southern Regional", "3300 Peachtree St, Atlanta, GA 30303"),
                new Warehouse("Great Lakes Facility", "9100 Lakefront Dr, Detroit, MI 48226"),
                new Warehouse("Mid-Atlantic Hub", "2800 Harbor Dr, Baltimore, MD 21230"),
                new Warehouse("Northern California", "5500 Tech Blvd, San Jose, CA 95110"),
                new Warehouse("Southern California", "7200 Pacific Coast Hwy, San Diego, CA 92101"),
                new Warehouse("Pacific Islands", "1900 Aloha Dr, Honolulu, HI 96815"),
                new Warehouse("Mountain Northwest", "4300 Cascade Ave, Portland, OR 97204"),
                new Warehouse("Central Plains", "6100 Prairie Way, Kansas City, MO 64105"),
                new Warehouse("Ohio Valley", "2500 River Rd, Cincinnati, OH 45202"),
                new Warehouse("Upper Midwest", "8400 Summit Ave, Minneapolis, MN 55402"),
                new Warehouse("Carolina Central", "3700 Capital Blvd, Raleigh, NC 27601"),
                new Warehouse("Gulf Coast", "5900 Bayou St, Houston, TX 77002"),
                new Warehouse("Desert Southwest", "4800 Mesa Dr, Las Vegas, NV 89101"),
                new Warehouse("Rocky Mountain", "7600 Alpine Way, Salt Lake City, UT 84101"),
                new Warehouse("Greater Philadelphia", "3200 Liberty Ave, Philadelphia, PA 19104"),
                new Warehouse("Northern Virginia", "9500 Capitol Way, Arlington, VA 22201")
            };

            foreach (var warehouse in warehouses)
            {
                warehouseService.Add(warehouse);
            }
        }
    }
}