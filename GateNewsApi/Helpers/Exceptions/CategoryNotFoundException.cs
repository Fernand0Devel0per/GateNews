using System.Runtime.Serialization;

namespace GateNewsApi.Helpers.Exceptions
{
    [Serializable]
    public class CategoryNotFoundException : Exception
    {
        public CategoryNotFoundException(int code)
            : base($"Category with code {code} not found.")
        {
        }

        protected CategoryNotFoundException(SerializationInfo info, StreamingContext context)
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
