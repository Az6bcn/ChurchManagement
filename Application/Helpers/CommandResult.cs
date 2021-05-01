using System.Collections.Generic;

namespace Application.Helpers
{
    public class CommandResult
    {
        private HashSet<string> _errors;
        private Dictionary<string, object> _errorDictionary;

        public CommandResult()
        {
            _errors = new();
            _errorDictionary = new();
        }

        internal CommandResult(bool success, int identityId, string error) : this()
        {
            Suceeded = success;
            IdentityId = identityId;
            Error = error;
        }

        internal CommandResult(bool success, int identityId, IEnumerable<string> errors) : this()
        {
            Suceeded = success;
            IdentityId = identityId;

            foreach (var error in errors)
                _errors.Add(error);
        }

        internal CommandResult(bool success, int identityId, IDictionary<string, object> errors) : this()
        {
            Suceeded = success;
            IdentityId = identityId;

            _errorDictionary = (Dictionary<string, object>)errors;
        }

        public bool Suceeded { get; set; }
        public int IdentityId { get; set; }
        public string Error { get; set; }
        public IReadOnlyCollection<string> ErrorsList => _errors;
        public IReadOnlyDictionary<string, object> ErrorsDictionary => _errorDictionary;

        public static CommandResult CreateCommandResult(bool success, int identityId, string error)
            => new CommandResult(success, identityId, error);

        public static CommandResult CreateCommandResult(bool success, int identityId, IEnumerable<string> errors)
            => new CommandResult(success, identityId, errors);

        public static CommandResult CreateCommandResult(bool success, int identityId, IDictionary<string, object> errors)
            => new CommandResult(success, identityId, errors);
    }
}
