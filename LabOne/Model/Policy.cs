using System;
using System.ComponentModel.DataAnnotations;

namespace LabOne
{
    /// <summary>
    /// Represents an insurance policy.
    /// </summary>
    public class Policy
    {
        public int PolicyId { get; set; }
        /// <summary>
        /// Gets or sets the email address associated with the policy.
        /// </summary>
        [Required, EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the account number associated with the policy.
        /// </summary>
        [Required, RegularExpression(@"^\d{8}$")]
        public string AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the policy number.
        /// </summary>
        [Required, RegularExpression(@"^\d{8}$")]
        public string PolicyNumber { get; set; }

        /// <summary>
        /// Gets or sets the first name of the policyholder.
        /// </summary>
        [Required, StringLength(50, MinimumLength = 1)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the middle name of the policyholder.
        /// </summary>
        [StringLength(50)]
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the policyholder.
        /// </summary>
        [Required, StringLength(50, MinimumLength = 1)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the postal code of the policyholder.
        /// </summary>
        [Required, RegularExpression(@"^\d{7}$")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the policyholder.
        /// </summary>
        [Required, Phone]
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the producer code.
        /// </summary>
        [Required, StringLength(5, MinimumLength = 1)]
        public string ProducerCode { get; set; }

        /// <summary>
        /// Gets or sets the group code.
        /// </summary>
        [Required, StringLength(5, MinimumLength = 1)]
        public string GroupCode { get; set; }

        /// <summary>
        /// Gets or sets the master code.
        /// </summary>
        [Required, StringLength(5, MinimumLength = 1)]
        public string MasterCode { get; set; }

        /// <summary>
        /// Gets or sets the city of the policyholder.
        /// </summary>
        [Required, StringLength(20, MinimumLength = 1)]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state of the policyholder.
        /// </summary>
        [Required, StringLength(20, MinimumLength = 1)]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the effective date of the policy.
        /// </summary>
        [Required, DataType(DataType.Date)]
        [CustomValidation(typeof(PolicyValidator), nameof(PolicyValidator.ValidateEffectiveDate))]
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// Gets or sets the expiration date of the policy.
        /// </summary>
        [Required, DataType(DataType.Date)]
        [CustomValidation(typeof(PolicyValidator), nameof(PolicyValidator.ValidateExpirationDate))]
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// Gets or sets the annual premium of the policy.
        /// </summary>
        [Required, Range(0.01, double.MaxValue)]
        [DataType(DataType.Currency)]
        public decimal AnnualPremium { get; set; }
    }

    /// <summary>
    /// Provides custom validation methods for the <see cref="Policy"/> class.
    /// </summary>
    public static class PolicyValidator
    {
        /// <summary>
        /// Validates the effective date of the policy.
        /// </summary>
        /// <param name="effectiveDate">The effective date to validate.</param>
        /// <param name="context">The validation context.</param>
        /// <returns>A <see cref="ValidationResult"/> indicating whether the effective date is valid.</returns>
        public static ValidationResult ValidateEffectiveDate(DateTime effectiveDate, ValidationContext context)
        {
            if (effectiveDate > DateTime.Now)
            {
                return new ValidationResult("Effective Date must be less than or equal to the current date.");
            }
            return ValidationResult.Success;
        }

        /// <summary>
        /// Validates the expiration date of the policy.
        /// </summary>
        /// <param name="expirationDate">The expiration date to validate.</param>
        /// <param name="context">The validation context.</param>
        /// <returns>A <see cref="ValidationResult"/> indicating whether the expiration date is valid.</returns>
        public static ValidationResult ValidateExpirationDate(DateTime expirationDate, ValidationContext context)
        {
            var instance = context.ObjectInstance as Policy;
            if (instance != null && expirationDate < instance.EffectiveDate)
            {
                return new ValidationResult("Expiration Date must be greater than or equal to the Effective Date.");
            }
            return ValidationResult.Success;
        }
    }
}
