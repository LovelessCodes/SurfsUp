using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SurfsUpBlazor.Shared;

namespace SurfsUpBlazor.Server.Data
{
    public class SurfboardConfiguration : IEntityTypeConfiguration<Surfboard>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Surfboard> builder)
        {
            builder.HasData(
                new Surfboard
                {
                    Id = 1,
                    ImageUrl = "https://www.light-surfboards.com/uploads/5/7/3/0/57306051/s326152794241300969_p19_i2_w320.jpeg",
                    Name = "The Minilog",
                    Thickness = 2.75F,
                    Width = 21,
                    Length = 6,
                    Volume = 38.8F,
                    Type = "Shortboard",
                    Price = 565,
                    Place = "Budapest"
                },
                new Surfboard
                {
                    Id = 2,
                    ImageUrl = "https://www.light-surfboards.com/uploads/5/7/3/0/57306051/s326152794241300969_p335_i10_w5000.jpeg?width=320",
                    Name = "The Wide Glider",
                    Thickness = 2.75F,
                    Width = 21.75F,
                    Length = 7.1F,
                    Volume = 44.16F,
                    Type = "Funboard",
                    Price = 685,
                    Place = "Rome"
                },
                new Surfboard
                {
                    Id = 3,
                    ImageUrl = "https://www.light-surfboards.com/uploads/5/7/3/0/57306051/s326152794241300969_p329_i37_w1168.png?width=320",
                    Name = "The Golden Ratio",
                    Thickness = 2.9F,
                    Width = 21.85F,
                    Length = 6.3F,
                    Volume = 43.22F,
                    Type = "Funboard",
                    Price = 695,
                    Place = "Amsterdam"
                },
                new Surfboard
                {
                    Id = 4,
                    ImageUrl = "https://www.light-surfboards.com/uploads/5/7/3/0/57306051/s326152794241300969_p332_i22_w1116.png?width=320",
                    Name = "Mahi Mahi",
                    Thickness = 2.3F,
                    Width = 20.75F,
                    Length = 5.4F,
                    Volume = 29.39F,
                    Type = "Fish",
                    Price = 645,
                    Place = "Berlin"
                },
                new Surfboard
                {
                    Id = 5,
                    ImageUrl = "https://www.light-surfboards.com/uploads/5/7/3/0/57306051/s326152794241300969_p336_i55_w5000.jpeg?width=320",
                    Name = "The Emerald Glider",
                    Thickness = 2.8F,
                    Width = 22.8F,
                    Length = 9.2F,
                    Volume = 65.4F,
                    Type = "Longboard",
                    Price = 895,
                    Place = "Stockholm"
                },
                new Surfboard
                {
                    Id = 6,
                    ImageUrl = "https://www.light-surfboards.com/uploads/5/7/3/0/57306051/s326152794241300969_p5_i4_w320.jpeg",
                    Name = "The Bomb",
                    Thickness = 2.5F,
                    Width = 21,
                    Length = 5.5F,
                    Volume = 33.7F,
                    Type = "Shortboard",
                    Price = 645,
                    Place = "Oslo"
                },
                new Surfboard
                {
                    Id = 7,
                    ImageUrl = "https://www.light-surfboards.com/uploads/5/7/3/0/57306051/s326152794241300969_p50_i28_w856.png?width=320",
                    Name = "Walden Magic",
                    Thickness = 3F,
                    Width = 19.4F,
                    Length = 9.6F,
                    Volume = 80F,
                    Type = "Longboard",
                    Price = 1025,
                    Place = "London"
                },
                new Surfboard
                {
                    Id = 8,
                    ImageUrl = "https://kite-prod.b-cdn.net/13844-home_default/naish-one-12-6-s26-inflatable-sup.jpg",
                    Name = "Naish One",
                    Thickness = 6F,
                    Width = 30,
                    Length = 12.6F,
                    Volume = 301F,
                    Type = "SUP",
                    Price = 854,
                    Place = "Malibu"
                },
                new Surfboard
                {
                    Id = 9,
                    ImageUrl = "https://kite-prod.b-cdn.net/16394-home_default/stx-tourer-11-6-2022-inflatable-sup-package.jpg",
                    Name = "STX Tourer",
                    Thickness = 6F,
                    Width = 32,
                    Length = 11.6F,
                    Volume = 270F,
                    Type = "SUP",
                    Price = 611,
                    Place = "Miami"
                },
                new Surfboard
                {
                    Id = 10,
                    ImageUrl = "https://kite-prod.b-cdn.net/14021-home_default/naish-maliko-14-0-x-23-carbon-s26-2022-sup.jpg",
                    Name = "Naish Maliko",
                    Thickness = 6F,
                    Width = 25,
                    Length = 14,
                    Volume = 330F,
                    Type = "SUP",
                    Price = 1304,
                    Place = "Copenhagen"
                }
            );
        }
    }
}
