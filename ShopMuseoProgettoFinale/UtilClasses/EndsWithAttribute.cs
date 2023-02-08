namespace ShopMuseoProgettoFinale.UtilClasses {
    /// <summary>
    /// Un'attributo di validazione per EF Core che si assicura che una proprietà di stringhe finisca con dei suffissi predefiniti.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class EndsWithAttribute : ValidationAttribute {
        public IEnumerable<string> ValidEnds { get; init; }

        /// <summary>
        /// Specifies a collection of suffixes, of which at least one must be the end of the field or property.
        /// </summary>
        /// <param name="validEnds"></param>
        /// <exception cref="ArgumentNullException">Thrown when no suffixes are passed or the given collection is null.</exception>
        /// <exception cref="ArgumentException">Thrown when there are empty suffixes in the collection.</exception>
        public EndsWithAttribute(IEnumerable<string> validEnds) {
            if (validEnds is null) throw new ArgumentNullException(nameof(validEnds), "The given collection cannot be null.");

            if (!validEnds.Any()) // Empty
                throw new ArgumentNullException(nameof(validEnds), "The attribute cannot have zero suffixes.");

            if (validEnds.Any(x => string.IsNullOrWhiteSpace(x))) // Any suffix is empty
                throw new ArgumentException("The attribute cannot have empty suffixes.", nameof(validEnds));

            ValidEnds = validEnds.Select(str => str.Trim());
        }

        /// <inheritdoc cref="EndsWithAttribute(IEnumerable{string})"/>
        /// <param name="validEnds"></param>
        public EndsWithAttribute(params string[] validEnds) : this(validEnds as IEnumerable<string>) { }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext) {
            if (value is not string castedValue) { return new ValidationResult("The given value is not a string."); }

            foreach (string validEnd in ValidEnds) {
                if (castedValue.EndsWith(validEnd)) { return ValidationResult.Success; }
            }

            return new ValidationResult($"The given value does not end in one of the following suffixes:" +
                $"{string.Join(", ", ValidEnds)}");
        }
    }
}