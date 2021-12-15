using System;
using System.Collections.Generic;

namespace Code.InterfaceAdapters
{
    public class Controller : IDisposable
    {
        protected List<IDisposable> _disposables = new List<IDisposable>();

        public virtual void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}