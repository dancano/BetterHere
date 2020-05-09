using BetterHere.Common.Enum;
using BetterHere.Web.Data.Entities;
using BetterHere.Web.Helpers;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetterHere.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;
        private readonly IBlobHelper _blobHelper;

        public SeedDb(
            DataContext dataContext,
            IUserHelper userHelper,
            IBlobHelper blobHelper)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
            _blobHelper = blobHelper;
        }

        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();
            await CheckTypeEsblishmentAsync();
            await CheckCitiesAsync();
            await CheckTypeFoodAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1010", "Daniel", "Cano", "ddcp10@gmail.com", "350 634 2747", "Calle Luna Calle Sol", UserType.Admin);
            UserEntity owner = await CheckUserAsync("2020", "Dario", "Cano", "danieldario_01@hotmail.com", "350 634 2747", "Calle Luna Calle Sol", UserType.Owner);
            UserEntity user1 = await CheckUserAsync("3030", "Daniel", "Peña", "probandos59@gmail.com", "350 634 2747", "Calle Luna Calle Sol", UserType.User);
            UserEntity user2 = await CheckUserAsync("4040", "Danidaniel", "Cape", "danielcano198367@correo.itm.edu.co", "350 634 2747", "Calle Luna Calle Sol", UserType.User);
            await CheckEstablishmentAsync(owner, user1, user2);
        }

        private async Task CheckCitiesAsync()
        {
            if (!_dataContext.Cities.Any())
            {
                _dataContext.Cities.Add(new CityEntity { Name = "Medellín" });
                _dataContext.Cities.Add(new CityEntity { Name = "Cali" });
                _dataContext.Cities.Add(new CityEntity { Name = "Bogotá D.C." });
                _dataContext.Cities.Add(new CityEntity { Name = "Santa Marta" });
                _dataContext.Cities.Add(new CityEntity { Name = "Cartagena" });
                _dataContext.Cities.Add(new CityEntity { Name = "Manizales" });
                _dataContext.Cities.Add(new CityEntity { Name = "Pereira" });
                _dataContext.Cities.Add(new CityEntity { Name = "Bello" });
                _dataContext.Cities.Add(new CityEntity { Name = "Valledupar" });

                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckTypeFoodAsync()
        {
            if (!_dataContext.TypeFoods.Any())
            {
                _dataContext.TypeFoods.Add(new TypeFoodEntity { FoodTypeName = "Italian" });
                _dataContext.TypeFoods.Add(new TypeFoodEntity { FoodTypeName = "Hot" });
                _dataContext.TypeFoods.Add(new TypeFoodEntity { FoodTypeName = "Arabic" });

                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckTypeEsblishmentAsync()
        {
            if (!_dataContext.TypeEstablishments.Any())
            {
                _dataContext.TypeEstablishments.Add(new TypeEstablishmentEntity { NameType = "Gourmet Food" });
                _dataContext.TypeEstablishments.Add(new TypeEstablishmentEntity { NameType = "Vegan Food" });
                _dataContext.TypeEstablishments.Add(new TypeEstablishmentEntity { NameType = "Fast Food" });

                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.Owner.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task<UserEntity> CheckUserAsync(
            string document,
            string firstName,
            string lastName,
            string email,
            string phone,
            string address,
            UserType userType)
        {
            UserEntity user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new UserEntity
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    UserType = userType
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

            return user;
        }

        private async Task CheckEstablishmentAsync(
            UserEntity owner,
            UserEntity user1,
            UserEntity user2)
        {
            if (!_dataContext.Establishments.Any())
            {
                _dataContext.Establishments.Add(new EstablishmentEntity
                {
                    User = user1,
                    Name = "D'Kache",
                    EstablishmentLocations = new List<EstablishmentLocationEntity>
                    {
                        new EstablishmentLocationEntity
                        {
                            Cities = await _dataContext.Cities.FirstOrDefaultAsync(c => c.Name == "Bello"),
                            TypeEstablishment = await _dataContext.TypeEstablishments.FirstOrDefaultAsync(te => te.NameType == "Fast Food"),
                            Foods = new List<FoodEntity>
                            {
                                new FoodEntity
                                {
                                    TypeFoods = await _dataContext.TypeFoods.FirstOrDefaultAsync(tf => tf.FoodTypeName == "Italian"),
                                    FoodName = "Albondigas bolognesas",
                                    Remarks = "Muy ricas, acompañadas de papas a la francesa",
                                    Qualification = 4.5f,
                                    User = user1
                                },
                                new FoodEntity
                                {
                                    TypeFoods = await _dataContext.TypeFoods.FirstOrDefaultAsync(tf => tf.FoodTypeName == "Arabic"),
                                    FoodName = "Shawarma",
                                    Remarks = "No agrado mucho, es demasiado condimentado, la presentación es buena",
                                    Qualification = 1.5f,
                                    User = user2
                                },
                                new FoodEntity
                                {
                                    TypeFoods = await _dataContext.TypeFoods.FirstOrDefaultAsync(tf => tf.FoodTypeName == "Hot"),
                                    FoodName = "Taquitos",
                                    Remarks = "Los más deliciosos que he probado, en realidad si son mexicanos y picantes",
                                    Qualification = 5.0f,
                                    User = user2
                                }
                            }
                        }
                    }
                });

                _dataContext.Establishments.Add(new EstablishmentEntity
                {
                    User = owner,
                    Name = "Madingas",
                    EstablishmentLocations = new List<EstablishmentLocationEntity>
                    {
                        new EstablishmentLocationEntity
                        {
                            Cities = await _dataContext.Cities.FirstOrDefaultAsync(c => c.Name == "Medellín"),
                            TypeEstablishment = await _dataContext.TypeEstablishments.FirstOrDefaultAsync(te => te.NameType == "Fast Food"),
                            Foods = new List<FoodEntity>
                            {
                                new FoodEntity
                                {
                                    TypeFoods = await _dataContext.TypeFoods.FirstOrDefaultAsync(tf => tf.FoodTypeName == "Italian"),
                                    FoodName = "Longuipapas",
                                    Remarks = "Es muy astiante la longaniza",
                                    Qualification = 3.5f,
                                    User = user1
                                },
                                new FoodEntity
                                {
                                    TypeFoods = await _dataContext.TypeFoods.FirstOrDefaultAsync(tf => tf.FoodTypeName == "Arabic"),
                                    FoodName = "MandiCostillas",
                                    Remarks = "Muy pocas costillas para su precio",
                                    Qualification = 2.5f,
                                    User = user2,
                                },
                                new FoodEntity
                                {
                                    TypeFoods = await _dataContext.TypeFoods.FirstOrDefaultAsync(tf => tf.FoodTypeName == "Hot"),
                                    FoodName = "Mandipollo",
                                    Remarks = "Economicas, buena porcion, y bien preparado el pollo",
                                    Qualification = 4.0f,
                                    User = user2
                                }
                            }
                        }
                    }
                });

                await _dataContext.SaveChangesAsync();
            }
        }

    }
}
