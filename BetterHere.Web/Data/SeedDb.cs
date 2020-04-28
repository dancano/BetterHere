using BetterHere.Common.Enum;
using BetterHere.Web.Data.Entities;
using BetterHere.Web.Helpers;
using Microsoft.EntityFrameworkCore;
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

        public SeedDb(
            DataContext dataContext,
            IUserHelper userHelper)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();
            await CheckTypeEsblishmentAsync();
            await CheckTypeFoodAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1010", "Daniel", "Cano", "ddcp10@gmail.com", "350 634 2747", "Calle Luna Calle Sol", UserType.Admin);
            var owner = await CheckUserAsync("2020", "Dario", "Cano", "danieldario_01@hotmail.com", "350 634 2747", "Calle Luna Calle Sol", UserType.Owner);
            var user1 = await CheckUserAsync("3030", "Daniel", "Peña", "probandos59@gmail.com", "350 634 2747", "Calle Luna Calle Sol", UserType.User);
            var user2 = await CheckUserAsync("4040", "Danidaniel", "Cape", "danielcano198367@correo.itm.edu.co", "350 634 2747", "Calle Luna Calle Sol", UserType.User);
            await CheckEstablishmentAsync(owner, user1, user2);
        }

        private async Task CheckTypeFoodAsync()
        {
            if (!_dataContext.TypeFoods.Any())
            {
                _dataContext.TypeFoods.Add(new TypeFoodEntity { FoodType = "Italiana" });
                _dataContext.TypeFoods.Add(new TypeFoodEntity { FoodType = "Mexicana" });
                _dataContext.TypeFoods.Add(new TypeFoodEntity { FoodType = "Peruana" });

                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckTypeEsblishmentAsync()
        {
            if (!_dataContext.TypeEstablishments.Any())
            {
                _dataContext.TypeEstablishments.Add(new TypeEstablishmentEntity { NameType = "Gourmet" });
                _dataContext.TypeEstablishments.Add(new TypeEstablishmentEntity { NameType = "Vegetariano" });
                _dataContext.TypeEstablishments.Add(new TypeEstablishmentEntity { NameType = "Comidas Rapidas" });

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
            var user = await _userHelper.GetUserByEmailAsync(email);
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
                    User = owner,
                    Name = "D'Kache",
                    TypeEstablishment = _dataContext.TypeEstablishments.FirstOrDefault(),
                    TypeFoods = new List<TypeFoodEntity>
                    {
                        new TypeFoodEntity
                        {
                            FoodType = _dataContext.TypeFoods.FirstOrDefaultAsync().ToString(),
                            Foods = new List<FoodEntity>
                            {
                                new FoodEntity
                                {
                                    FoodName = "Enchilados",
                                    Qualification = 4.5f,
                                    Remarks = "Muy sabrosas, recomendadas",
                                    User = user1
                                },
                                new FoodEntity
                                {
                                    FoodName = "Quesadillas",
                                    Qualification = 2.5f,
                                    Remarks = "Muy saladas, no las recomiendo",
                                    User = user1
                                },
                                new FoodEntity
                                {
                                    FoodName = "Papitas Fritas",
                                    Qualification = 5.0f,
                                    Remarks = "Las mejores de la ciudad, recomendisimas",
                                    User = user2
                                }
                            }
                        }
                    }
                });

                _dataContext.Establishments.Add(new EstablishmentEntity
                {
                    User = user1,
                    Name = "Mandingas",
                    TypeEstablishment = _dataContext.TypeEstablishments.FirstOrDefault(),
                    TypeFoods = new List<TypeFoodEntity>
                    {
                        new TypeFoodEntity
                        {
                            FoodType = _dataContext.TypeFoods.FirstOrDefaultAsync().ToString(),
                            Foods = new List<FoodEntity>
                            {
                                new FoodEntity
                                {
                                    FoodName = "Longuipapas",
                                    Qualification = 4.5f,
                                    Remarks = "Muy sabrosas, recomendadas",
                                    User = user2
                                },
                                new FoodEntity
                                {
                                    FoodName = "Mandipollo",
                                    Qualification = 2.5f,
                                    Remarks = "Muy saladas, no las recomiendo",
                                    User = user1
                                },
                                new FoodEntity
                                {
                                    FoodName = "Mandingas Especial",
                                    Qualification = 5.0f,
                                    Remarks = "Las mejores de la ciudad, recomendisimas",
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
