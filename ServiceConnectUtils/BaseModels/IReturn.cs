﻿namespace ServiceConnectUtils.BaseModels
{
    public interface IReturn { }
    public interface IReturn<T> : IReturn { }

    public interface IReturnVoid : IReturn { }
}
