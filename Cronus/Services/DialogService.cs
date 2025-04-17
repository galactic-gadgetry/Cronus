using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Effects;
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
