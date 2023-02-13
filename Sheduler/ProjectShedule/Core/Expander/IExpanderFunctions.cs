using System;

namespace ProjectShedule.Core.Expander
{
    public interface IExpanderInfo
    {
        bool CanExpand();
    }

    public class ExpanderControll : IExpanderInfo
    {
        private readonly Func<bool> _canExpand;
        public ExpanderControll(Func<bool> canExpand)
        {
            _canExpand = canExpand;
        }

        public bool CanExpand() => _canExpand.Invoke();
    }
}

