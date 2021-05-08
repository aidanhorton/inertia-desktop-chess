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

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.AddHandler(ToggleButton.ClickEvent, new RoutedEventHandler(AssociatedObject_Click), true);
        }

        void AssociatedObject_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.button = sender as ToggleButton;
            if (this.button != null && this.button.ContextMenu != null)
            {
                if (!isContextMenuOpen)
                {
                    // Add handler to detect when the ContextMenu closes
                    this.button.ContextMenu.AddHandler(ContextMenu.ClosedEvent, new RoutedEventHandler(ContextMenu_Closed), true);
                    // If there is a drop-down assigned to this button, then position and display it 
                    this.button.ContextMenu.PlacementTarget = this.button;
                    this.button.ContextMenu.Placement = PlacementMode.Bottom;
                    this.button.ContextMenu.IsOpen = true;
                    isContextMenuOpen = true;
                }
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.RemoveHandler(ToggleButton.ClickEvent, new RoutedEventHandler(AssociatedObject_Click));
        }

        private void ContextMenu_Closed(object sender, RoutedEventArgs e)
        {
            if (this.button != null)
            {
                this.button.IsChecked = false;
            }

            isContextMenuOpen = false;
            var contextMenu = sender as ContextMenu;
            if (contextMenu != null)
            {
                contextMenu.RemoveHandler(ContextMenu.ClosedEvent, new RoutedEventHandler(ContextMenu_Closed));
            }
        }
    }
}
