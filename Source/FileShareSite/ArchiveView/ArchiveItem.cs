using System.Text;

namespace FileShareSite
{
    public abstract class ArchiveItem
    {
        private string _fullName;

        public ArchiveDirectory Parent { get; }
        public string Name { get; }
        public string FullName
        {
            get
            {
                if (_fullName == null)
                    _fullName = CreateFullName();
                return _fullName;
            }
        }

        protected ArchiveItem(ArchiveDirectory parent, string name)
        {
            Parent = parent;
            Name = name;
        }

        protected virtual string CreateFullName()
        {
            if (Parent == null)
                return Name;

            int nameLength = Name.Length;
            RecursiveGetNameLength(ref nameLength, Parent);

            var builder = new StringBuilder(nameLength);

            // start the recursion
            if (Parent != null)
                RecursiveAppend(builder, Parent);

            builder.Append(Name);
            return builder.ToString();
        }

        private static void RecursiveAppend(StringBuilder builder, ArchiveDirectory parent)
        {
            if (parent.Parent != null)
            {
                // calling this function again before appending the current
                // name creates the full name by unwinding the hierarchy
                RecursiveAppend(builder, parent.Parent);

                builder.Append(parent.Name);
                builder.Append('/');
            }
        }

        private static void RecursiveGetNameLength(ref int value, ArchiveDirectory parent)
        {
            if (parent.Parent != null)
            {
                RecursiveGetNameLength(ref value, parent.Parent);

                value += parent.Name.Length + 1;
            }
        }
    }
}
