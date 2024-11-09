// <copyright file="ICommand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    /// <summary>
    /// Defines the interface for command objects.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Gets the title of the command for display purposes.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Executes the command.
        /// </summary>
        void Execute();

        /// <summary>
        /// Unexecutes (reverses) the command.
        /// </summary>
        void Undo();
    }
}