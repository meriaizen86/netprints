﻿using System;
using System.Runtime.Serialization;

namespace NetPrints.Core
{
    /// <summary>
    /// Modifiers variables can have. Can be combined.
    /// </summary>
    [Flags]
    public enum VariableModifiers
    {
        None = 0,
        ReadOnly = 8,
        Const = 16,
        Static = 32,
        New = 64,

        [Obsolete]
        Private = 0,
        [Obsolete]
        Public = 1,
        [Obsolete]
        Protected = 2,
        [Obsolete]
        Internal = 4,
    }

    /// <summary>
    /// Specifier describing a property of a class.
    /// </summary>
    [Serializable]
    [DataContract(Name = "PropertySpecifier")]
    public class Variable
    {
        /// <summary>
        /// Name of the variable without any prefixes.
        /// </summary>
        [DataMember]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Class this variable is contained in.
        /// </summary>
        [DataMember]
        public Class Class
        {
            get;
            private set;
        }

        /// <summary>
        /// Specifier for the type of the variable.
        /// </summary>
        [DataMember]
        public TypeSpecifier Type
        {
            get;
            set;
        }

        /// <summary>
        /// Get method for this variable. Can be null.
        /// </summary>
        [DataMember]
        public Method GetterMethod
        {
            get;
            set;
        }

        /// <summary>
        /// Set method for this variable. Can be null.
        /// </summary>
        [DataMember]
        public Method SetterMethod
        {
            get;
            set;
        }

        /// <summary>
        /// Whether this variable has a public getter.
        /// </summary>
        public bool HasPublicGetter
        {
            get => HasAccessors ?
                (GetterMethod?.Visibility == MemberVisibility.Public) :
                Visibility == MemberVisibility.Public;
        }

        /// <summary>
        /// Whether this variable has a public setter.
        /// </summary>
        public bool HasPublicSetter
        {
            get => HasAccessors ?
                (SetterMethod?.Visibility == MemberVisibility.Public) :
                Visibility == MemberVisibility.Public;
        }

        /// <summary>
        /// Whether this variable declares a get or set method.
        /// </summary>
        public bool HasAccessors
        {
            get => GetterMethod != null || SetterMethod != null;
        }

        /// <summary>
        /// Whether this property is static.
        /// </summary>
        [DataMember]
        [Obsolete]
        public bool IsStatic
        {
            get;
            private set;
        }

        /// <summary>
        /// Visibility of this property.
        /// </summary>
        [DataMember]
        public MemberVisibility Visibility
        {
            get;
            set;
        } = MemberVisibility.Private;

        /// <summary>
        /// Modifiers of this variable.
        /// </summary>
        [DataMember]
        public VariableModifiers Modifiers
        {
            get;
            set;
        }

        public VariableSpecifier Specifier
        {
            get => new VariableSpecifier(Name, Type, GetterMethod?.Visibility ?? Visibility, SetterMethod?.Visibility ?? Visibility, Class.Type, Modifiers);
        }

        /// <summary>
        /// Creates a PropertySpecifier.
        /// </summary>
        /// <param name="name">Name of the property.</param>
        /// <param name="type">Specifier for the type of this property.</param>
        /// <param name="getter">Get method for the property. Can be null if there is none.</param>
        /// <param name="setter">Set method for the property. Can be null if there is none.</param>
        /// <param name="declaringType">Specifier for the type the property is contained in.</param>
        public Variable(Class cls, string name, TypeSpecifier type, Method getter,
            Method setter, VariableModifiers modifiers)
        {
            Class = cls;
            Name = name;
            Type = type;
            GetterMethod = getter;
            SetterMethod = setter;
            Modifiers = modifiers;
        }
    }
}
