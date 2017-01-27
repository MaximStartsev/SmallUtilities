using System;

namespace MaximStartsev.GamepadRemoteControl.Meta
{
    /// <summary>
    /// На данный момент все параметры команд должны быть типа String
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    class CommandParameterAttribute:Attribute
    {
    }
}
