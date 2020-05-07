using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;

namespace ThreeShape.Identity.Security
{
    public class Claims : IEnumerable<Claim>
    {
        private readonly List<Claim> _claims;

        public Claims(IEnumerable<Claim> claims)
            : this(claims.ToList())
        {
        }

        public Claims(List<Claim> claims)
        {
            _claims = claims;
        }

        public T GetClaimAs<T>(string name)
        {
            var claim = _claims.FirstOrDefault(x => x.Type == name);

            return ConvertOrDefault<T>(claim);
        }

        public IEnumerable<T> GetClaimsAs<T>(string name)
        {
            var claims = _claims.Where(x => x.Type == name);
            return claims.Select(x => ConvertOrDefault<T>(x));
        }

        public IEnumerator<Claim> GetEnumerator() => _claims.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _claims.GetEnumerator();

        private T ConvertOrDefault<T>(Claim claim)
        {
            if (claim is null)
            {
                return default(T);
            }

            return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(claim.Value);
        }
    }
}
