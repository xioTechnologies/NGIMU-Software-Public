using System.Collections.Generic;

namespace NgimuApi
{
    public abstract class SettingCategrory : ISettingItem
    {
        internal List<ISettingValue> AllValues = new List<ISettingValue>();

        private List<ISettingItem> items = new List<ISettingItem>();

        private List<ISettingValue> settings = new List<ISettingValue>();

        /// <summary>
        /// Gets all ISettingItem items in this category that are not hidden. 
        /// </summary>
        public IEnumerable<ISettingItem> Items { get; protected set; }

        /// <summary>
        /// Gets all ISettingValue items in this category and all child categories.
        /// </summary>
        public IEnumerable<ISettingValue> Values { get; protected set; }

        /// <summary>
        /// Gets a flag indicating that this category is bound to a connection.
        /// </summary>
        public bool HasConnection { get { return Connection != null; } }

        /// <summary>
        /// Gets the Connection that this setting category is bound to or null if it is unbound.
        /// </summary>
        public abstract Connection Connection { get; }

        /// <summary>
        /// Gets the parent category of this category or null if this category is the root category.
        /// </summary>
        public abstract SettingCategrory Parent { get; }

        /// <summary>
        /// Gets the category text including the full path from the root category.
        /// </summary>
        public abstract string Text { get; }

        /// <summary>
        /// Gets the category prefix text.
        /// </summary>
        protected abstract int CategoryPrefix { get; }

        /// <summary>
        /// Gets a flag to indicate if this category is hidden.
        /// </summary>
        public abstract bool IsHidden { get; }

        /// <summary>
        /// Gets the text value of this category.
        /// </summary>
        /// <returns>The text value of this category.</returns>
        public override string ToString()
        {
            return Text.ToString();
        }

        /// <summary>
        /// Gets the text of the category. 
        /// </summary>
        public string CategoryText { get { return new string('\t', CategoryPrefix) + Text; } }

        internal SettingCategrory()
        {

        }

        protected string LookupDocumentation(string address)
        {
            return SettingsDocumentation.GetSettingDocumentation(address);
        }

        internal abstract void AttachSettings(Settings settings);

        protected void Finalise()
        {
            Values = settings.ToArray();
            Items = items.ToArray();
        }

        protected void Add(ISettingItem item)
        {
            if (item.IsHidden == false)
            {
                items.Add(item);
            }

            if (item is ISettingValue)
            {
                if (item.IsHidden == false)
                {
                    settings.Add(item as ISettingValue);
                }

                AllValues.Add(item as ISettingValue);
            }
            else if (item is SettingCategrory)
            {
                if (item.IsHidden == false)
                {
                    settings.AddRange((item as SettingCategrory).settings);
                }

                AllValues.AddRange((item as SettingCategrory).AllValues);
            }
        }

        public virtual CommunicationProcessResult Read(int timeout = 100, int retryLimit = 3)
        {
            return Read(null, timeout, retryLimit);
        }

        public virtual CommunicationProcessResult Read(IReporter reporter, int timeout = 100, int retryLimit = 3)
        {
            return Parent.Read(settings.ToArray(), reporter, timeout, retryLimit);
        }

        public virtual CommunicationProcessResult Read(params ISettingItem[] items)
        {
            return Parent.Read(items);
        }

        public virtual CommunicationProcessResult Read(ISettingItem[] items, IReporter reporter = null, int timeout = 100, int retryLimit = 3)
        {
            return Parent.Read(items, reporter, timeout, retryLimit);
        }

        public virtual void ReadAync(int timeout = 100, int retryLimit = 3)
        {
            ReadAync(null, timeout, retryLimit);
        }

        public virtual void ReadAync(IReporter reporter, int timeout = 100, int retryLimit = 3)
        {
            Parent.ReadAync(settings.ToArray(), reporter, timeout, retryLimit);
        }

        public virtual void ReadAync(params ISettingItem[] items)
        {
            Parent.ReadAync(items);
        }

        public virtual void ReadAync(ISettingItem[] items, IReporter reporter = null, int timeout = 100, int retryLimit = 3)
        {
            Parent.ReadAync(items, reporter, timeout, retryLimit);
        }

        public virtual CommunicationProcessResult Write(int timeout = 100, int retryLimit = 3)
        {
            return Write(null, timeout, retryLimit);
        }

        public virtual CommunicationProcessResult Write(IReporter reporter, int timeout = 100, int retryLimit = 3)
        {
            return Parent.Write(settings.ToArray(), reporter, timeout, retryLimit);
        }

        public virtual CommunicationProcessResult Write(params ISettingItem[] items)
        {
            return Parent.Write(items);
        }

        public virtual CommunicationProcessResult Write(ISettingItem[] items, IReporter reporter = null, int timeout = 100, int retryLimit = 3)
        {
            return Parent.Write(items, reporter, timeout, retryLimit);
        }

        public virtual void WriteAync(int timeout = 100, int retryLimit = 3)
        {
            WriteAync(null, timeout, retryLimit);
        }

        public virtual void WriteAync(IReporter reporter, int timeout = 100, int retryLimit = 3)
        {
            Parent.WriteAync(settings.ToArray(), reporter, timeout, retryLimit);
        }

        public virtual void WriteAync(params ISettingItem[] items)
        {
            Parent.WriteAync(items);
        }

        public virtual void WriteAync(ISettingItem[] items, IReporter reporter = null, int timeout = 100, int retryLimit = 3)
        {
            Parent.WriteAync(items, reporter, timeout, retryLimit);
        }
    }
}
