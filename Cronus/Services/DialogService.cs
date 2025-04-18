using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Effects;
using Cronus.Models;
using Cronus.UIComponents.Dialogs;

namespace Cronus.Services
{
    public static class DialogService
    {
        /// <summary>
        /// Dim the window visuals.
        /// </summary>
        /// <param name="window"></param>
        public static void DimWindowVisuals(Window window)
        {
            ArgumentNullException.ThrowIfNull(window, nameof(window));

            // Dim and blur the window.
            window.Opacity = 0.2;
            window.Effect = new BlurEffect();
        }

        /// <summary>
        /// Displays the <see cref="CreateNewLogBookDialog"/> dialog
        /// window.
        /// </summary>
        /// <returns>The Create New Log Book dialog window</returns>
        public static CreateNewLogBookDialog PromptUserWithCreateNewLogBookDialog()
        {
            CreateNewLogBookDialog dlg = new();
            Window mainWindow = Application.Current.MainWindow;
            dlg.Owner = mainWindow;
            dlg.NameText = "New log book";

            // Dim the window.
            DimWindowVisuals(mainWindow);

            dlg.ShowDialog();

            // Restore the main window.
            RestoreWindowVisuals(mainWindow);

            return dlg;
        }

        /// <summary>
        /// Displays the <see cref="ConfirmationDialog"/> window.
        /// </summary>
        /// <param name="model">Model instance to be deleted</param>
        /// <returns>The Confirmation Dialog window</returns>
        public static ConfirmationDialog PromptUserWithDeleteConfirmationDialog(
            IConfirmDeletion model)
        {
            ArgumentNullException.ThrowIfNull(model, nameof(model));

            ConfirmationDialog dlg = new("DELETE");
            Window mainWindow = Application.Current.MainWindow;
            dlg.Owner = mainWindow;
            dlg.TitleText = $"Delete {model.GetModelTypeString()}";
            dlg.ConfirmationMessageText = "Are you sure that you " +
                $"want to delete this {model.GetModelTypeString().ToLower()}? " +
                "This action cannot be undone.";
            dlg.ConfirmationControlLabelText = "TYPE 'DELETE' TO " +
                "CONFIRM";
            dlg.DialogAcceptButtonText = $"Delete {model.GetModelTypeString()}";

            // Dim the main window.
            DimWindowVisuals(mainWindow);

            dlg.ShowDialog();

            // Restore the main window.
            RestoreWindowVisuals(mainWindow);

            return dlg;
        }

        /// <summary>
        /// Returns the window visuals to normal.
        /// </summary>
        /// <param name="window"></param>
        public static void RestoreWindowVisuals(Window window)
        {
            ArgumentNullException.ThrowIfNull(window, nameof(window));

            // Set window properties to normal.
            window.Opacity = 1;
            window.Effect = null;
        }
    }
}
