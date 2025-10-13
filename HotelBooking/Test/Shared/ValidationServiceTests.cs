using Shared.Interfaces;
using Shared.Interfaces.Services;
using Shared.Models;
using Shared.Models.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Entity;

namespace Test.Shared
{
    public class ValidationServiceTests
    {
        #region Private Properties
        public static IEnumerable<object[]> EmptyStringProperties
        {
            get
            {
                yield return new object[] { "", "", "", new List<string> { 
                                                                            string.Format(ValidationError.Template, nameof(User.FirstName) ,ValidationError.IsEmpty), 
                                                                            string.Format(ValidationError.Template, nameof(User.LastName), ValidationError.IsEmpty),
                                                                            string.Format(ValidationError.Template, nameof(User.Address) ,ValidationError.IsEmpty)
                                                                         } };
                yield return new object[] { "", "Holland", "", new List<string> { 
                                                                                    string.Format(ValidationError.Template, nameof(User.FirstName) ,ValidationError.IsEmpty), 
                                                                                    string.Format(ValidationError.Template, nameof(User.Address) ,ValidationError.IsEmpty)
                                                                                } };
                yield return new object[] { "", "", "Bøfvænget", new List<string> {
                                                                                    string.Format(ValidationError.Template, nameof(User.FirstName) ,ValidationError.IsEmpty),
                                                                                    string.Format(ValidationError.Template, nameof(User.LastName), ValidationError.IsEmpty),
                                                                                  } };
                yield return new object[] { "Firstname", "", "", new List<string> {
                                                                                    string.Format(ValidationError.Template, nameof(User.LastName), ValidationError.IsEmpty),
                                                                                    string.Format(ValidationError.Template, nameof(User.Address) ,ValidationError.IsEmpty)
                                                                                  } };
                
            }
        }
        public static IEnumerable<object[]> WhitespaceProperties
        {
            get
            {
                yield return new object[] { " ", " ", " " , new List<string> {
                                                                            string.Format(ValidationError.Template, nameof(User.FirstName) ,ValidationError.IsOnlyWhitespace),
                                                                            string.Format(ValidationError.Template, nameof(User.LastName), ValidationError.IsOnlyWhitespace),
                                                                            string.Format(ValidationError.Template, nameof(User.Address) ,ValidationError.IsOnlyWhitespace)
                                                                           } };
                yield return new object[] { "Bøf", " ", " " , new List<string> {
                                                                            string.Format(ValidationError.Template, nameof(User.LastName), ValidationError.IsOnlyWhitespace),
                                                                            string.Format(ValidationError.Template, nameof(User.Address) ,ValidationError.IsOnlyWhitespace)
                                                                           } };
                yield return new object[] { " ", "Bøf", " " , new List<string> {
                                                                            string.Format(ValidationError.Template, nameof(User.FirstName) ,ValidationError.IsOnlyWhitespace),
                                                                            string.Format(ValidationError.Template, nameof(User.Address) ,ValidationError.IsOnlyWhitespace)
                                                                           } };
                yield return new object[] { " ", " ", "Bøf" , new List<string> {
                                                                            string.Format(ValidationError.Template, nameof(User.FirstName) ,ValidationError.IsOnlyWhitespace),
                                                                            string.Format(ValidationError.Template, nameof(User.LastName) ,ValidationError.IsOnlyWhitespace)
                                                                           } };
                yield return new object[] { " ", "Bøf", "Løg" , new List<string> {
                                                                                string.Format(ValidationError.Template, nameof(User.FirstName) ,ValidationError.IsOnlyWhitespace)
                                                                           } };
                yield return new object[] { "Bøf", " ", "Løg", new List<string> {
                                                                                string.Format(ValidationError.Template, nameof(User.LastName) ,ValidationError.IsOnlyWhitespace)
                                                                           } };
                yield return new object[] { "Bøf", "Løg", " " , new List<string> {
                                                                                string.Format(ValidationError.Template, nameof(User.Address) ,ValidationError.IsOnlyWhitespace)
                                                                           } };
            }
        }

        public static IEnumerable<object[]> NullProperties
        {
            get
            {
                yield return new object[] { null, null, null, new List<string> {
                                                                            string.Format(ValidationError.Template, nameof(User.FirstName) ,ValidationError.IsNull),
                                                                            string.Format(ValidationError.Template, nameof(User.LastName), ValidationError.IsNull),
                                                                            string.Format(ValidationError.Template, nameof(User.Address) ,ValidationError.IsNull)
                                                                           } };
                yield return new object[] { "Holland", null, null, new List<string> {
                                                                            string.Format(ValidationError.Template, nameof(User.LastName), ValidationError.IsNull),
                                                                            string.Format(ValidationError.Template, nameof(User.Address) ,ValidationError.IsNull)
                                                                           } }; 
                yield return new object[] { null, "Holland", null , new List<string> {
                                                                            string.Format(ValidationError.Template, nameof(User.FirstName) ,ValidationError.IsNull),
                                                                            string.Format(ValidationError.Template, nameof(User.Address) ,ValidationError.IsNull)
                                                                           } };
                yield return new object[] { null, null, "Holland", new List<string> {
                                                                            string.Format(ValidationError.Template, nameof(User.FirstName) ,ValidationError.IsNull),
                                                                            string.Format(ValidationError.Template, nameof(User.LastName), ValidationError.IsNull)
                                                                           } };
                yield return new object[] { null, "Holland", "Holland", new List<string> {
                                                                            string.Format(ValidationError.Template, nameof(User.FirstName) ,ValidationError.IsNull)
                                                                           } };
                yield return new object[] { "Holland", null, "Holland", new List<string> {
                                                                            string.Format(ValidationError.Template, nameof(User.LastName) ,ValidationError.IsNull)
                                                                           } };
                yield return new object[] { "Holland", "Holland", null, new List<string> {
                                                                            string.Format(ValidationError.Template, nameof(User.Address) ,ValidationError.IsNull)
                                                                           } };
            }
        }

        #endregion

        /// <summary>
        /// Test for the method ValidateNullEmptyOrWhitespace
        /// <para>Not failing</para>
        /// </summary>
        [Fact]
        public void ValidateNullEmptyOrWhitespace_NoNullAndEmpty_ShouldReturnTrue()
        {
            // Arrange
            User user = new User
            {
                Address = "Test af adresse",
                FirstName = "Tom test",
                LastName = "Holland test"
            };
            IValidationService validateService = new ValidationService();

            List<string> propNames = new List<string> 
            {
                nameof(User.FirstName), 
                nameof(User.LastName), 
                nameof(User.Address)
            };

            // Action
            ValidationResponse result = validateService.ValidateNullEmptyOrWhitespace<User>(user, propNames);

            // Assert
            Assert.True(result.Result);
            Assert.Null(result.Errors);
        }

        /// <summary>
        /// Test for the method ValidateNullEmptyOrWhitespace
        /// <para>Not failing due to null</para>
        /// </summary>
        [Theory]
        [MemberData(nameof(NullProperties))]
        public void ValidateNullEmptyOrWhitespace_GivesNull_ShouldReturnFalseAndError(string firstName, string lastName, string address, List<string> errors)
        {
            // Arrange
            User user = new User
            {
                Address = address,
                FirstName = firstName,
                LastName = lastName
            };
            IValidationService validateService = new ValidationService();

            List<string> propNames = new List<string>
            {
                nameof(User.FirstName),
                nameof(User.LastName),
                nameof(User.Address)
            };

            // Action
            ValidationResponse result = validateService.ValidateNullEmptyOrWhitespace<User>(user, propNames);


            // Assert
            Assert.False(result.Result);
            Assert.Equal(errors, result.Errors);
        }

        /// <summary>
        /// Test for the method ValidateNullEmptyOrWhitespace
        /// <para>Not failing due to empty</para>
        /// </summary>
        [Theory]
        [MemberData(nameof(EmptyStringProperties))]
        public void ValidateNullEmptyOrWhitespace_GivesEmpty_ShouldReturnFalseAndError(string firstName, string lastName, string address, List<string> errors)
        {
            // Arrange
            User user = new User
            {
                Address = address,
                FirstName = firstName,
                LastName = lastName
            };
            IValidationService validateService = new ValidationService();

            List<string> propNames = new List<string>
            {
                nameof(User.FirstName),
                nameof(User.LastName),
                nameof(User.Address)
            };

            // Action
            ValidationResponse result = validateService.ValidateNullEmptyOrWhitespace<User>(user, propNames);


            // Assert
            Assert.False(result.Result);
            Assert.NotNull(result.Errors);
            foreach (var error in errors)
            {
                result.Errors.Contains(error);
            }
            Assert.Equal(errors, result.Errors);
        }

        /// <summary>
        /// Test for the method ValidateNullEmptyOrWhitespace
        /// <para>Not failing due to whitespace</para>
        /// </summary>
        [Theory]
        [MemberData(nameof(EmptyStringProperties))]
        public void ValidateNullEmptyOrWhitespace_GivesWhitespace_ShouldReturnFalseAndError(string firstName, string lastName, string address, List<string> errors)
        {
            // Arrange
            User user = new User
            {
                Address = address,
                FirstName = firstName,
                LastName = lastName
            };
            IValidationService validateService = new ValidationService();

            List<string> propNames = new List<string>
            {
                nameof(User.FirstName),
                nameof(User.LastName),
                nameof(User.Address)
            };

            // Action
            ValidationResponse result = validateService.ValidateNullEmptyOrWhitespace<User>(user, propNames);


            // Assert
            Assert.False(result.Result);
            Assert.NotNull(result.Errors);
            foreach (var error in errors)
            {
                result.Errors.Contains(error);
            }
            Assert.Equal(errors, result.Errors);
        }
    }
}
