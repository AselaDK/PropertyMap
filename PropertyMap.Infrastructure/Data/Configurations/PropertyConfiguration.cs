using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyMap.Core.Entities;

namespace PropertyMap.Infrastructure.Data.Configurations
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Description)
                .HasMaxLength(2000);

            builder.Property(p => p.Price)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(p => p.Address)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.State)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.ZipCode)
                .HasMaxLength(20);

            builder.Property(p => p.Latitude)
                .IsRequired();

            builder.Property(p => p.Longitude)
                .IsRequired();

            builder.Property(p => p.PropertyType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.ImageUrl)
                .HasMaxLength(500);

            // Seed UK properties
            var seedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            
            builder.HasData(
                new Property { Id = 1, Title = "Luxury Chelsea Apartment", Description = "Stunning 2-bed apartment in the heart of Chelsea.", Price = 1200000m, Address = "King's Road", City = "London", State = "Greater London", ZipCode = "SW3 4TR", Latitude = 51.4875, Longitude = -0.1687, PropertyType = "Apartment", Bedrooms = 2, Bathrooms = 2, SquareFeet = 950, YearBuilt = 2018, ImageUrl = "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688", CreatedAt = seedDate },
                new Property { Id = 2, Title = "Manchester Northern Quarter Loft", Description = "Trendy loft with exposed brickwork.", Price = 350000m, Address = "Tib Street", City = "Manchester", State = "Greater Manchester", ZipCode = "M4 1SH", Latitude = 53.4839, Longitude = -2.2352, PropertyType = "Loft", Bedrooms = 1, Bathrooms = 1, SquareFeet = 700, YearBuilt = 1910, ImageUrl = "https://images.unsplash.com/photo-1568605114967-8130f3a36994", CreatedAt = seedDate },
                new Property { Id = 3, Title = "Edinburgh New Town Flat", Description = "Classic Georgian flat with high ceilings.", Price = 550000m, Address = "George Street", City = "Edinburgh", State = "Midlothian", ZipCode = "EH2 2PB", Latitude = 55.9533, Longitude = -3.1883, PropertyType = "Flat", Bedrooms = 3, Bathrooms = 2, SquareFeet = 1400, YearBuilt = 1850, ImageUrl = "https://images.unsplash.com/photo-1512917774080-9991f1c4c750", CreatedAt = seedDate },
                new Property { Id = 4, Title = "Birmingham Jewellery Quarter Studio", Description = "Modern studio in a historic district.", Price = 180000m, Address = "Warstone Lane", City = "Birmingham", State = "West Midlands", ZipCode = "B18 6HP", Latitude = 52.4862, Longitude = -1.8904, PropertyType = "Studio", Bedrooms = 0, Bathrooms = 1, SquareFeet = 450, YearBuilt = 2022, ImageUrl = "https://images.unsplash.com/photo-1560448204-e02f11c3d0e2", CreatedAt = seedDate },
                new Property { Id = 5, Title = "Bristol Harbourside Condo", Description = "Waterfront living with balcony views.", Price = 425000m, Address = "Canons Way", City = "Bristol", State = "Bristol", ZipCode = "BS1 5LL", Latitude = 51.4545, Longitude = -2.5879, PropertyType = "Condo", Bedrooms = 2, Bathrooms = 1, SquareFeet = 850, YearBuilt = 2015, ImageUrl = "https://images.unsplash.com/photo-1484154218962-a197022b5858", CreatedAt = seedDate },
                new Property { Id = 6, Title = "Oxford Victorian Terrace", Description = "Charming period home near the university.", Price = 750000m, Address = "Cowley Road", City = "Oxford", State = "Oxfordshire", ZipCode = "OX4 1HU", Latitude = 51.7520, Longitude = -1.2577, PropertyType = "House", Bedrooms = 3, Bathrooms = 1, SquareFeet = 1100, YearBuilt = 1890, ImageUrl = "https://images.unsplash.com/photo-1570129477492-45c003edd2be", CreatedAt = seedDate },
                new Property { Id = 7, Title = "Cambridge Tech Park Apartment", Description = "Ultra-modern apartment for young professionals.", Price = 380000m, Address = "Milton Road", City = "Cambridge", State = "Cambridgeshire", ZipCode = "CB4 1ST", Latitude = 52.2053, Longitude = 0.1218, PropertyType = "Apartment", Bedrooms = 2, Bathrooms = 2, SquareFeet = 800, YearBuilt = 2021, ImageUrl = "https://images.unsplash.com/photo-1523217582562-09d0def993a6", CreatedAt = seedDate },
                new Property { Id = 8, Title = "Brighton Seafront Penthouse", Description = "Luxury living with panoramic ocean views.", Price = 950000m, Address = "Marine Parade", City = "Brighton", State = "East Sussex", ZipCode = "BN2 1TL", Latitude = 50.8225, Longitude = -0.1372, PropertyType = "Penthouse", Bedrooms = 3, Bathrooms = 3, SquareFeet = 1600, YearBuilt = 2019, ImageUrl = "https://images.unsplash.com/photo-1512918728675-ed5a9ecdebfd", CreatedAt = seedDate },
                new Property { Id = 9, Title = "Liverpool Dockside Apartment", Description = "Renovated warehouse in Albert Dock.", Price = 280000m, Address = "The Strand", City = "Liverpool", State = "Merseyside", ZipCode = "L3 4AF", Latitude = 53.4084, Longitude = -2.9916, PropertyType = "Apartment", Bedrooms = 2, Bathrooms = 1, SquareFeet = 750, YearBuilt = 1860, ImageUrl = "https://images.unsplash.com/photo-1580587771525-78b9dba3b914", CreatedAt = seedDate },
                new Property { Id = 10, Title = "Leeds City Square Apartment", Description = "High-spec living in the city center.", Price = 240000m, Address = "Wellington Street", City = "Leeds", State = "West Yorkshire", ZipCode = "LS1 4DL", Latitude = 53.7997, Longitude = -1.5491, PropertyType = "Apartment", Bedrooms = 1, Bathrooms = 1, SquareFeet = 600, YearBuilt = 2017, ImageUrl = "https://images.unsplash.com/photo-1564013799919-ab600027ffc6", CreatedAt = seedDate },
                new Property { Id = 11, Title = "Glasgow West End Flat", Description = "Traditional sandstone tenement flat.", Price = 320000m, Address = "Byres Road", City = "Glasgow", State = "Lanarkshire", ZipCode = "G12 8AU", Latitude = 55.8642, Longitude = -4.2518, PropertyType = "Flat", Bedrooms = 2, Bathrooms = 1, SquareFeet = 900, YearBuilt = 1900, ImageUrl = "https://images.unsplash.com/photo-1598228723793-52759bba239c", CreatedAt = seedDate },
                new Property { Id = 12, Title = "Cardiff Bay Waterfront", Description = "Modern apartment with views of the bay.", Price = 210000m, Address = "Harbour Drive", City = "Cardiff", State = "South Glamorgan", ZipCode = "CF10 4PA", Latitude = 51.4816, Longitude = -3.1791, PropertyType = "Apartment", Bedrooms = 1, Bathrooms = 1, SquareFeet = 550, YearBuilt = 2014, ImageUrl = "https://images.unsplash.com/photo-1576013551627-0cc20b96c2a7", CreatedAt = seedDate },
                new Property { Id = 13, Title = "Belfast Titanic Quarter", Description = "Stylish city living in a landmark location.", Price = 225000m, Address = "Queens Road", City = "Belfast", State = "County Antrim", ZipCode = "BT3 9DT", Latitude = 54.5973, Longitude = -5.9301, PropertyType = "Apartment", Bedrooms = 2, Bathrooms = 2, SquareFeet = 820, YearBuilt = 2016, ImageUrl = "https://images.unsplash.com/photo-1493809842364-78817add7ffb", CreatedAt = seedDate },
                new Property { Id = 14, Title = "Nottingham Lace Market Flat", Description = "Converted factory space with huge windows.", Price = 195000m, Address = "Stoney Street", City = "Nottingham", State = "Nottinghamshire", ZipCode = "NG1 1LH", Latitude = 52.9548, Longitude = -1.1581, PropertyType = "Flat", Bedrooms = 1, Bathrooms = 1, SquareFeet = 680, YearBuilt = 1880, ImageUrl = "https://images.unsplash.com/photo-1512915922686-57c11dde9b6b", CreatedAt = seedDate },
                new Property { Id = 15, Title = "Newcastle Quayside Studio", Description = "Overlooking the Tyne Bridge.", Price = 150000m, Address = "Sandhill", City = "Newcastle", State = "Tyne and Wear", ZipCode = "NE1 3AF", Latitude = 54.9783, Longitude = -1.6174, PropertyType = "Studio", Bedrooms = 0, Bathrooms = 1, SquareFeet = 400, YearBuilt = 2012, ImageUrl = "https://images.unsplash.com/photo-1560185127-6ed189bf02f4", CreatedAt = seedDate },
                new Property { Id = 16, Title = "Sheffield Eco-House", Description = "Sustainable living with solar panels.", Price = 310000m, Address = "Ecclesall Road", City = "Sheffield", State = "South Yorkshire", ZipCode = "S11 8NX", Latitude = 53.3811, Longitude = -1.4701, PropertyType = "House", Bedrooms = 3, Bathrooms = 2, SquareFeet = 1250, YearBuilt = 2023, ImageUrl = "https://images.unsplash.com/photo-1518780664697-55e3ad937233", CreatedAt = seedDate },
                new Property { Id = 17, Title = "Bath Crescent View", Description = "Luxury apartment in a heritage building.", Price = 675000m, Address = "Royal Crescent", City = "Bath", State = "Somerset", ZipCode = "BA1 2LS", Latitude = 51.3814, Longitude = -2.3590, PropertyType = "Apartment", Bedrooms = 2, Bathrooms = 2, SquareFeet = 1050, YearBuilt = 1770, ImageUrl = "https://images.unsplash.com/photo-1516455590571-18256e5bb9ff", CreatedAt = seedDate },
                new Property { Id = 18, Title = "York Minster Cottage", Description = "Quaint cottage in the medieval Shambles.", Price = 420000m, Address = "Low Petergate", City = "York", State = "North Yorkshire", ZipCode = "YO1 7HZ", Latitude = 53.9591, Longitude = -1.0812, PropertyType = "House", Bedrooms = 2, Bathrooms = 1, SquareFeet = 880, YearBuilt = 1650, ImageUrl = "https://images.unsplash.com/photo-1448630360428-65456885c650", CreatedAt = seedDate },
                new Property { Id = 19, Title = "Cotswolds Stone Barn", Description = "Rural retreat with acres of land.", Price = 895000m, Address = "Burford Road", City = "Cotswolds", State = "Gloucestershire", ZipCode = "GL54 1AF", Latitude = 51.8330, Longitude = -1.7480, PropertyType = "House", Bedrooms = 4, Bathrooms = 3, SquareFeet = 2200, YearBuilt = 1800, ImageUrl = "https://images.unsplash.com/photo-1500382017468-9049fed747ef", CreatedAt = seedDate },
                new Property { Id = 20, Title = "Greenwich Observatory Flat", Description = "Overlooking the Royal Park.", Price = 480000m, Address = "Greenwich High Road", City = "London", State = "Greater London", ZipCode = "SE10 8NN", Latitude = 51.4826, Longitude = -0.0077, PropertyType = "Apartment", Bedrooms = 2, Bathrooms = 1, SquareFeet = 780, YearBuilt = 2005, ImageUrl = "https://images.unsplash.com/photo-1522708323590-d24dbb6b0267", CreatedAt = seedDate },
                new Property { Id = 21, Title = "Cornwall Coastal Retreat", Description = "Beachfront house in St Ives.", Price = 725000m, Address = "Fore Street", City = "St Ives", State = "Cornwall", ZipCode = "TR26 1AB", Latitude = 50.2116, Longitude = -5.4800, PropertyType = "House", Bedrooms = 3, Bathrooms = 2, SquareFeet = 1350, YearBuilt = 1950, ImageUrl = "https://images.unsplash.com/photo-1499793983690-e29da59ef1c2", CreatedAt = seedDate }
            );
        }
    }
}
