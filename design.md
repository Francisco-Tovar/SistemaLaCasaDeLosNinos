# Design Rules: Modern UI for WinForms

## 1. Form Configuration & Behavior
To achieve a modern look while maintaining native Windows functionality (Aero Snap, resizing, and shadows), the form must handle specific low-level messages.

*   **Borderless Setup:** Set `FormBorderStyle` to `None`.
*   **Maintain Native Features:** Use the `WndProc` method to intercept Windows messages. This allows for native shadows and Aero Snap without the default title bar.
*   **WM_NCCALCSIZE (0x0083):** Return `IntPtr.Zero` when the window is being sized to preserve the client area while removing the standard frame.
*   **WM_NCHITTEST (0x0084):** Map mouse positions to border regions (`HTLEFT`, `HTRIGHT`, `HTBOTTOM`, etc.) to enable manual resizing of the borderless form.
*   **Dragging Logic:** Import `user32.dll` to use `ReleaseCapture()` and `SendMessage(Handle, 0x112, 0xf012, 0)` on the Title Bar's `MouseDown` event.
*   **Maximize Fix:** To prevent the maximized form from covering the taskbar, set `this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea` during the `Load` event.

## 2. Layout Structure
The interface follows a hierarchical docking strategy to ensure responsiveness.

| Component | Docking | Purpose |
| :--- | :--- | :--- |
| **MenuPanel** | Left | Contains the logo and navigation buttons. |
| **TitleBarPanel** | Top | Contains the form title, drag area, and control box (Min/Max/Close). |
| **ContentPanel** | Fill | Container for switching child forms using `.BringToFront()`. |
| **LogoPanel** | Top (Inside Menu) | Displays the brand/app icon; usually fixed height. |

## 3. Sliding Menu Mechanics
The side menu transitions between two states to maximize screen real estate.

*   **Collapsed State:** Width of ~60px. Only icons from FontAwesome.Sharp are visible.
*   **Expanded State:** Width of ~220px. Icons and text labels are visible.
*   **Toggle Logic:** Clicking the "Hamburger" button toggles the width. For smooth transitions, use a Timer to increment/decrement the width, or a simple boolean toggle for instant switching.
*   **Dropdown Menus:** Use a Panel or FlowLayoutPanel container for sub-menu items. Toggle the `Visible` property or adjust `Height` to show/hide sub-items.

## 4. Visual Styles & Components
*   **Color Palette:**
    *   **Dark Theme:** Primary Dark (`#1F1F2F`), Secondary Dark (`#2D2D3F`), Accent Cyan/Blue (`#00A2E8`).
*   **Transparency:** Avoid `TransparencyKey` as it causes flickering. Use solid colors and clean panels.
*   **Icons:** Utilize **FontAwesome.Sharp** NuGet package for vector-based icons that scale cleanly.
*   **Active State:** Highlight the current active button by changing its `BackColor` or adding a small vertical Panel (2px wide) to its left edge.

## 5. Technical Implementation Code
The following snippet outlines the core logic for the custom form behavior.

```csharp
protected override void WndProc(ref Message m)
{
    const int WM_NCCALCSIZE = 0x0083;
    const int WM_NCHITTEST = 0x0084;
    const int resizeAreaSize = 10;

    // Remove title bar but keep snap/shadow
    if (m.Msg == WM_NCCALCSIZE && m.WParam.ToInt32() == 1)
    {
        return;
    }

    // Custom resizing
    if (m.Msg == WM_NCHITTEST)
    {
        base.WndProc(ref m);
        if ((int)m.Result == 1) // HTCLIENT
        {
            Point screenPoint = new Point(m.LParam.ToInt32());
            Point clientPoint = this.PointToClient(screenPoint);

            if (clientPoint.Y <= resizeAreaSize)
            {
                if (clientPoint.X <= resizeAreaSize) m.Result = (IntPtr)13; // HTTOPLEFT
                else if (clientPoint.X >= (this.Size.Width - resizeAreaSize)) m.Result = (IntPtr)14; // HTTOPRIGHT
                else m.Result = (IntPtr)12; // HTTOP
            }
            else if (clientPoint.Y >= (this.Size.Height - resizeAreaSize))
            {
                if (clientPoint.X <= resizeAreaSize) m.Result = (IntPtr)16; // HTBOTTOMLEFT
                else if (clientPoint.X >= (this.Size.Width - resizeAreaSize)) m.Result = (IntPtr)17; // HTBOTTOMRIGHT
                else m.Result = (IntPtr)15; // HTBOTTOM
            }
            else if (clientPoint.X <= resizeAreaSize) m.Result = (IntPtr)10; // HTLEFT
            else if (clientPoint.X >= (this.Size.Width - resizeAreaSize)) m.Result = (IntPtr)11; // HTRIGHT
        }
        return;
    }
    base.WndProc(ref m);
}
```

## 6. Responsive Rules
*   **Anchoring:** Use `Anchor` properties for controls inside the `ContentPanel` to handle stretching.
*   **Docking:** Always dock the main containers (Left, Top, Fill).
*   **Form Loading:** Clear `ContentPanel.Controls` before adding a new form and set `childForm.TopLevel = false`, `childForm.FormBorderStyle = None`, and `childForm.Dock = Fill`.
