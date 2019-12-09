using System;

namespace D.MoveOn.Common.Mvc
{
    public class ServiceInfor : IServiceInfor
    {
        private static readonly string _id = $"serviceid:{Guid.NewGuid():N}";
        public string Id => _id;
    }
}