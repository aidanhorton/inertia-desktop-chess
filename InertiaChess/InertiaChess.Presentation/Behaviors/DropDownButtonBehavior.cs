using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace InertiaChess.Presentation.Behaviors
{
    public class DropDownButtonBehavior : Behavior<ToggleButton>
    {
        private ToggleButton button;
        private bool isContextMenuOpen;

        private void AssociatedObject_Click(object sender, RoutedEventArgs e)
        {
            this.button = sender as ToggleButton;
            if (this.button == null || this.button.ContextMenu == null) return;

            if (!this.isContextMenuOpen)
            {
                // Add handler to detect when the ContextMenu closes
                this.button.ContextMenu.AddHandler(ContextMenu.ClosedEvent, new RoutedEventHandler(this.ContextMenu_Closed), true);

                // If there is a drop-down assigned to this button, then position and display it 
                this.button.ContextMenu.PlacementTarget = this.button;
                this.button.ContextMenu.Placement = PlacementMode.Bottom;
                this.button.ContextMenu.IsOpen = true;

                this.isContextMenuOpen = true;
            }
        }

        private void ContextMenu_Closed(object sender, RoutedEventArgs e)
        {
            if (this.button != null)
            {
                this.button.IsChecked = false;
            }

            this.isContextMenuOpen = false;

            if (sender is ContextMenu contextMenu)
            {
                contextMenu.RemoveHandler(ContextMenu.ClosedEvent, new RoutedEventHandler(this.ContextMenu_Closed));
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.AddHandler(ToggleButton.ClickEvent, new RoutedEventHandler(this.AssociatedObject_Click), true);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.RemoveHandler(ToggleButton.ClickEvent, new RoutedEventHandler(this.AssociatedObject_Click));
        }
    }
}
