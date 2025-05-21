// using System;
// using System.ComponentModel.DataAnnotations;
// using System.Threading.Tasks;
// using System.Security.Cryptography;
// using System.Text;
// using MusicStore.BusinessLogic.Data.DataInterfaces;
// using MusicStore.BusinessLogic.Interfaces;
// using MusicStore.BusinessLogic.Services.Interfaces;
// using MusicStore2.Domain.Entities.User;
//
// namespace MusicStore.BusinessLogic.Services
// {
//     public class AuthService : IAuthService
//     {
//         private readonly IUserRepository _userRepository;
//
//         public AuthService(IUserRepository userRepository)
//         {
//             _userRepository = userRepository;
//         }
//         public async Task<string> CreateUserSessionAsync(int userId)
//         {
//             var token = Guid.NewGuid().ToString();
//
//             var session = new UserSession
//             {
//                 UserId = userId,
//                 Token = token,
//                 CreatedAt = DateTime.UtcNow,
//                 ExpiresAt = DateTime.UtcNow.AddHours(2) // sesiune valabilă 2h
//             };
//
//             await _userRepository.AddUserSessionAsync(session);
//
//             return token;
//         }
//
//
//         public async Task<UserAuthResp> UserLoginActionAsync(UserLoginData data)
//         {
//             if (data == null)
//             {
//                 return new UserAuthResp
//                 {
//                     Status = false,
//                     StatusMsg = "Date de autentificare invalide."
//                 };
//             }
//
//             try
//             {
//                 if (string.IsNullOrEmpty(data.Email))
//                 {
//                     return new UserAuthResp
//                     {
//                         Status = false,
//                         StatusMsg = "Adresa de email este necesară."
//                     };
//                 }
//
//                 var user = await _userRepository.GetUserByEmailAsync(data.Email);
//                 //if (user == null)
//                 //{
//                 //    return new UserAuthResp
//                 //    {
//                 //        Status = false,
//                 //        StatusMsg = "Utilizator inexistent sau parolă incorectă."
//                 //    };
//                 //}
//
//                 string hashedPassword = ComputeHash(data.Password);
//                 if (user.PasswordHash != hashedPassword)
//                 {
//                     return new UserAuthResp
//                     {
//                         Status = false,
//                         StatusMsg = "Utilizator inexistent sau parolă incorectă."
//                     };
//                 }
//
//
//                 await _userRepository.UpdateUserAsync(user.Email);
//
//                 return new UserAuthResp
//                 {
//                     Status = true,
//                     StatusMsg = "Autentificare reușită.",
//                     Id = user.Id,
//                     Email = user.Email,
//                     UserName = user.Name,
//                     UserRole = user.UserRole,
//                     LoginDateTime = user.LastLoginTime,
//                     Token = user.Token
//                 };
//             }
//             catch (Exception ex)
//             {
//                 return new UserAuthResp
//                 {
//                     Status = false,
//                     StatusMsg = "A apărut o eroare la autentificare. Încercați din nou."
//                 };
//             }
//         }
//
//         public async Task<UserAuthResp> UserRegisterActionAsync(UserRegData data)
//         {
//             if (data == null)
//             {
//                 return new UserAuthResp
//                 {
//                     Status = false,
//                     StatusMsg = "Date de înregistrare invalide."
//                 };
//             }
//
//             try
//             {
//                 if (string.IsNullOrEmpty(data.Email) || string.IsNullOrEmpty(data.Password) ||
//                     string.IsNullOrEmpty(data.Name) || string.IsNullOrEmpty(data.UserRole))
//                 {
//                     return new UserAuthResp
//                     {
//                         Status = false,
//                         StatusMsg = "Toate câmpurile sunt obligatorii."
//                     };
//                 }
//
//                 var existingUser = await _userRepository.GetUserByEmailAsync(data.Email);
//                 if (existingUser != null)
//                 {
//                     return new UserAuthResp
//                     {
//                         Status = false,
//                         StatusMsg = "Această adresă de email este deja asociată unui cont."
//                     };
//                 }
//
//                 string hashedPassword = ComputeHash(data.Password);
//
//                 var newUser = new AppUser
//                 {
//                     Email = data.Email,
//                     Name = data.Name,
//                     PasswordHash = hashedPassword,
//                     UserRole = data.UserRole,
//                     LastLoginTime = DateTime.Now,
//                     Token = Guid.NewGuid().ToString()
//                 };
//
//                 await _userRepository.AddUserAsync(newUser);
//
//                 return new UserAuthResp
//                 {
//                     Status = true,
//                     StatusMsg = "Cont creat cu succes!",
//                     Email = newUser.Email,
//                     UserName = newUser.Name,
//                     UserRole = newUser.UserRole,
//                     LoginDateTime = newUser.LastLoginTime,
//                     Token = newUser.Token
//                 };
//             }
//             catch (Exception ex)
//             {
//                 return new UserAuthResp
//                 {
//                     Status = false,
//                     StatusMsg = "A apărut o eroare la crearea contului. Încercați din nou."
//                 };
//             }
//         }
//
//         public static string ComputeHash(string password)
//         {
//             using (var sha256 = SHA256.Create())
//             {
//                 var bytes = Encoding.UTF8.GetBytes(password);
//                 var hash = sha256.ComputeHash(bytes);
//                 return Convert.ToBase64String(hash);
//             }
//         }
//     }
// }