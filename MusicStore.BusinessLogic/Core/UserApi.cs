﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MusicStore.BusinessLogic.Data;
using MusicStore2.Domain.Entities.Cart;
using MusicStore2.Domain.Entities.Product;
using MusicStore2.Domain.Entities.User;

namespace MusicStore.BusinessLogic.Core
{
    public class UserApi
    {
        private readonly AppDbContext _context = new AppDbContext();


        internal async Task<ProductData> GetByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }


        internal IEnumerable<ProductData> GetAllAsync()
        {
            return _context.Products.AsNoTracking().ToList();
        }

        internal Task CreateAsync(ProductData productData)
        {
            if (productData == null)
            {
                throw new ArgumentException("Product cannot be null");
            }


            _context.Products.Add(productData);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        internal async Task UpdateProductAsync(ProductData productData)
        {
            if (productData == null)
            {
                throw new ArgumentNullException(nameof(productData), "Utilizatorul nu poate fi null.");
            }

            var existingProduct = _context.Products.FirstOrDefault(u => u.Id == productData.Id);

            if (existingProduct == null)
            {
                throw new InvalidOperationException("Utilizatorul nu a fost găsit.");
            }


            existingProduct.Name = productData.Name;
            existingProduct.AudioFileUrl = productData.AudioFileUrl;
            existingProduct.ImageUrl = productData.ImageUrl;
            existingProduct.Bpm = productData.Bpm;
            existingProduct.Genre = productData.Genre;
            existingProduct.Scale = productData.Scale;
            existingProduct.Price = productData.Price;


            _context.Entry(existingProduct).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        internal void DeleteProductById(int productId)
        {
            var entity = _context.Products.FirstOrDefault(p => p.Id == productId);
            ;
            if (entity == null)
            {
                throw new ArgumentException("Product not found");
            }

            _context.Products.Remove(entity);
            _context.SaveChanges();
        }

        internal async Task<string> CreateUserSessionAsync(int userId)
        {
            var token = Guid.NewGuid().ToString();

            var session = new UserSession
            {
                UserId = userId,
                Token = token,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddHours(2) // sesiune valabilă 2h
            };

            _context.UserSessions.Add(session);
            await _context.SaveChangesAsync();

            return token;
        }

        internal async Task UpdateUserAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user != null)
            {
                DateTime newLoginTime = DateTime.UtcNow;
                string token = Guid.NewGuid().ToString();

                user.LastLoginTime = newLoginTime;
                user.Token = token;

                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            else
            {
            }
        }

        internal async Task<UserAuthResp> UserLoginActionAsync(UserLoginData data, string dataEmail)
        {
            if (data == null)
            {
                return new UserAuthResp
                {
                    Status = false,
                    StatusMsg = "Date de autentificare invalide."
                };
            }

            try
            {
                if (string.IsNullOrEmpty(data.Email))
                {
                    return new UserAuthResp
                    {
                        Status = false,
                        StatusMsg = "Adresa de email este necesară."
                    };
                }

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dataEmail);
                if (user == null)
                {
                    return new UserAuthResp
                    {
                        Status = false,
                        StatusMsg = "Utilizator inexistent."
                    };
                }

                string hashedPassword = ComputeHash(data.Password);
                if (user.PasswordHash != hashedPassword)
                {
                    return new UserAuthResp
                    {
                        Status = false,
                        StatusMsg = "Utilizator inexistent sau parolă incorectă."
                    };
                }

                await UpdateUserAsync(user.Email);


                user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dataEmail);

                return new UserAuthResp
                {
                    Status = true,
                    StatusMsg = "Autentificare reușită.",
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.Name,
                    UserRole = user.UserRole,
                    LoginDateTime = user.LastLoginTime,
                    Token = user.Token
                };
            }
            catch (Exception)
            {
                return new UserAuthResp
                {
                    Status = false,
                    StatusMsg = "A apărut o eroare la autentificare. Încercați din nou."
                };
            }
        }


        internal async Task<UserAuthResp> UserRegisterActionAsync(UserRegData data, string email)
        {
            if (data == null)
            {
                return new UserAuthResp
                {
                    Status = false,
                    StatusMsg = "Date de înregistrare invalide."
                };
            }

            try
            {
                if (string.IsNullOrEmpty(data.Email) || string.IsNullOrEmpty(data.Password) ||
                    string.IsNullOrEmpty(data.Name) || string.IsNullOrEmpty(data.UserRole))
                {
                    return new UserAuthResp
                    {
                        Status = false,
                        StatusMsg = "Toate câmpurile sunt obligatorii."
                    };
                }

                var existingUser = _context.Users.FirstOrDefault(u => u.Email == email);
                if (existingUser != null)
                {
                    return new UserAuthResp
                    {
                        Status = false,
                        StatusMsg = "Această adresă de email este deja asociată unui cont."
                    };
                }

                string hashedPassword = ComputeHash(data.Password);

                var newUser = new AppUser
                {
                    Email = data.Email,
                    Name = data.Name,
                    PasswordHash = hashedPassword,
                    UserRole = data.UserRole,
                    LastLoginTime = DateTime.Now,
                    Token = Guid.NewGuid().ToString()
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                return new UserAuthResp
                {
                    Status = true,
                    StatusMsg = "Cont creat cu succes!",
                    Email = newUser.Email,
                    UserName = newUser.Name,
                    UserRole = newUser.UserRole,
                    LoginDateTime = newUser.LastLoginTime,
                    Token = newUser.Token
                };
            }
            catch (Exception ex)
            {
                return new UserAuthResp
                {
                    Status = false,
                    StatusMsg = "A apărut o eroare la crearea contului. Încercați din nou."
                };
            }
        }

        public static string ComputeHash(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        internal void SaveChanges()
        {
            _context.SaveChanges();
        }





        protected async Task<IEnumerable<CartItem>> GetUserCartItemsAsync(int userId)
        {
            using (var db = new AppDbContext())
            {
                return await db.UserCartItems
                    .Include(ci => ci.Product)
                    .Where(ci => ci.UserId == userId)
                    .Select(ci => new CartItem
                    {
                        SongId = ci.ProductId,
                        SongName = ci.Product.Name,
                        ArtistName = ci.Product.ArtistName,
                        Price = ci.Product.Price,
                        FileName = ci.Product.AudioFileUrl
                    })
                    .ToListAsync();
            }
        }

        protected async Task AddItemToCartAsync(int userId, int productId)
        {
            using (var db = new AppDbContext())
            {
                var item = new UserCartItem
                {
                    UserId = userId,
                    ProductId = productId
                };
                db.UserCartItems.Add(item);
                await db.SaveChangesAsync();
            }
        }

        protected async Task RemoveItemFromCartAsync(int userId, int productId)
        {
            using (var db = new AppDbContext())
            {
                var item = await db.UserCartItems
                    .FirstOrDefaultAsync(ci => ci.UserId == userId && ci.ProductId == productId);

                if (item != null)
                {
                    db.UserCartItems.Remove(item);
                    await db.SaveChangesAsync();
                }
            }
        }





        //search song
        protected async Task<IEnumerable<ProductData>> SearchByNameOrArtistAsync(string query)
        {
            using (var context = new AppDbContext())
            {
                return await context.Products
                    .Where(p => p.Name.Contains(query) || p.ArtistName.Contains(query))
                    .ToListAsync();
            }
        }
        
        
        // Change Password
        internal async Task<UserAuthResp> UpdateUserPassword(string email,string newPassword)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
                if (user == null)
                {
                    return new UserAuthResp
                    {
                        Status = false,
                        StatusMsg = "Utilizatorul nu există."
                    };
                }

                user.PasswordHash = ComputeHash(newPassword);
                await _context.SaveChangesAsync();

                return new UserAuthResp
                {
                    Status = true,
                    StatusMsg = "Parola a fost actualizată cu succes."
                };
            }
            catch (Exception)
            {
                return new UserAuthResp
                {
                    Status = false,
                    StatusMsg = "Eroare la actualizarea parolei."
                };
            }
        }


    }
}

