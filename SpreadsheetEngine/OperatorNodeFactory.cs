// <copyright file="OperatorNodeFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Factory class to create operator nodes based on the operator character.
    /// </summary>
    internal class OperatorNodeFactory
    {
        // Dictionary to map operator symbols to their corresponding OperatorNode types
        private readonly Dictionary<char, Type> operators = new Dictionary<char, Type>();

        // Delegate definition for operator traversal
        private delegate void OnOperator(char op, Type type);

        /// <summary>
        /// Initializes a new instance of the OperatorNodeFactory class.
        /// Uses reflection to populate the operator dictionary.
        /// </summary>
        public OperatorNodeFactory()
        {
            // Instantiate the delegate with a lambda that adds to the operators dictionary
            this.TraverseAvailableOperators((op, type) => this.operators.Add(op, type));
        }

        /// <summary>
        /// Creates an operator node based on the operator character.
        /// </summary>
        /// <param name="op">The operator character.</param>
        /// <param name="left">The left operand node.</param>
        /// <param name="right">The right operand node.</param>
        /// <returns>The operator node.</returns>
        public OperatorNode CreateOperatorNode(char op, Node left, Node right)
        {
            if (this.operators.ContainsKey(op))
            {
                Type operatorType = this.operators[op];

                // Create an instance of the operator node with left and right operands
                object operatorNodeObject = Activator.CreateInstance(operatorType, left, right);
                if (operatorNodeObject is OperatorNode operatorNode)
                {
                    return operatorNode;
                }
            }

            throw new InvalidOperationException($"Unhandled operator: '{op}'.");
        }

        /// <summary>
        /// Traverses all available operator nodes using reflection and invokes the provided delegate.
        /// </summary>
        /// <param name="onOperator">Delegate to handle each operator symbol and type.</param>
        private void TraverseAvailableOperators(OnOperator onOperator)
        {
            // Get the type declaration of OperatorNode
            Type operatorNodeType = typeof(OperatorNode);

            // Iterate over all loaded assemblies
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                // Get all types that inherit from OperatorNode using LINQ
                IEnumerable<Type> operatorTypes = assembly.GetTypes()
                    .Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(operatorNodeType));

                // Iterate over those subclasses of OperatorNode
                foreach (var type in operatorTypes)
                {
                    // Retrieve the static "Operator" property
                    PropertyInfo operatorProperty = type.GetProperty("Operator", BindingFlags.Public | BindingFlags.Static);

                    if (operatorProperty != null && operatorProperty.PropertyType == typeof(char))
                    {
                        // Get the operator symbol
                        object value = operatorProperty.GetValue(null);
                        if (value is char operatorSymbol)
                        {
                            // Invoke the delegate with the operator symbol and type
                            onOperator(operatorSymbol, type);
                        }
                        else
                        {
                            throw new InvalidOperationException($"The 'Operator' property in {type.Name} does not return a char.");
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException($"The class {type.Name} must have a public static property 'Operator' of type char.");
                    }
                }
            }
        }
    }
}