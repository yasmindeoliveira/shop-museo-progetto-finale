namespace ShopMuseoProgettoFinale.Models {
    /// <summary>
    /// Un'attributo di validazione per EF Core che si assicura che una proprietà di stringhe finisca con dei suffissi predefiniti.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class EndsWithAttribute : ValidationAttribute {
        public IEnumerable<string> ValidEnds { get; init; }
        public EndsWithAttribute(IEnumerable<string> validEnds) {
            if (!validEnds.Any()) // Empty
                throw new ArgumentNullException(nameof(validEnds), "L'attributo non può avere zero suffissi.");

            if (validEnds.Any(x => string.IsNullOrWhiteSpace(x))) // Any suffix is empty
                throw new ArgumentException("L'attributo non può avere stringhe vuote.", nameof(validEnds));

            ValidEnds = validEnds.Select(str => str.Trim());
        }

        public EndsWithAttribute(params string[] validEnds) : this(validEnds as IEnumerable<string>) { }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext) {
            if (value is not string castedValue) { return new ValidationResult("Il valore inserito non è una stringa."); }

            foreach (string validEnd in ValidEnds) {
                if (castedValue.EndsWith(validEnd)) { return ValidationResult.Success; }
            }

            return new ValidationResult($"Il valore inserito non finisce con una delle seguenti stringhe:" +
                $"{string.Join(", ", ValidEnds)}");
        }
    }
}
