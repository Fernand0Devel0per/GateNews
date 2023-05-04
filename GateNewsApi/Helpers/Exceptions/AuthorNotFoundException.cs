using System.Runtime.Serialization;

namespace GateNewsApi.Helpers.Exceptions
{
    [Serializable]
    public class AuthorNotFoundException : Exception
    {
        public AuthorNotFoundException(string message)
            : base(message)
        {
        }

        protected AuthorNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            base.GetObjectData(info, context);
        }
    }

}
