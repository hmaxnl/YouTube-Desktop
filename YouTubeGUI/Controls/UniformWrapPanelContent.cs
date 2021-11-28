using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using YouTubeScrap.Data.Extend;

namespace YouTubeGUI.Controls
{
    public class UniformWrapPanelContent : WrapPanel
    {
        /// <summary>
        /// Defines the <see cref="Rows"/> property.
        /// </summary>
        public static readonly StyledProperty<int> RowsProperty =
            AvaloniaProperty.Register<UniformWrapPanelContent, int>(nameof(Rows));

        /// <summary>
        /// Defines the <see cref="Columns"/> property.
        /// </summary>
        public static readonly StyledProperty<int> ColumnsProperty =
            AvaloniaProperty.Register<UniformWrapPanelContent, int>(nameof(Columns));

        /// <summary>
        /// Defines the <see cref="FirstColumn"/> property.
        /// </summary>
        public static readonly StyledProperty<int> FirstColumnProperty =
            AvaloniaProperty.Register<UniformWrapPanelContent, int>(nameof(FirstColumn));
        
        private int _rows;
        private int _columns;

        public UniformWrapPanelContent()
        {
            AffectsMeasure<UniformWrapPanelContent>(RowsProperty, ColumnsProperty, FirstColumnProperty);
        }
        
        /// <summary>
        /// Specifies the row count. If set to 0, row count will be calculated automatically.
        /// </summary>
        public int Rows
        {
            get => GetValue(RowsProperty);
            set => SetValue(RowsProperty, value);
        }

        /// <summary>
        /// Specifies the column count. If set to 0, column count will be calculated automatically.
        /// </summary>
        public int Columns
        {
            get => GetValue(ColumnsProperty);
            set => SetValue(ColumnsProperty, value);
        }

        /// <summary>
        /// Specifies, for the first row, the column where the items should start.
        /// </summary>
        public int FirstColumn
        {
            get => GetValue(FirstColumnProperty);
            set => SetValue(FirstColumnProperty, value);
        }
        
        // Check if to use this child.
        private bool CheckChild(IControl child)
        {
            var dContext = child.DataContext;
            if (dContext != null && dContext.GetType() == typeof(ContentRender))
            {
                ContentRender cRend = (ContentRender)dContext;
                if (cRend.RichSection != null || cRend.ChipCloudChip != null)
                    return true;
            }
            return false;
        }
        
        protected override Size MeasureOverride(Size availableSize)
        {
            return base.MeasureOverride(availableSize);
        }

        private void UpdateRowsAndColumns()
        {
            _rows = Rows;
            _columns = Columns;

            if (FirstColumn >= Columns)
            {
                FirstColumn = 0;
            }

            var itemCount = FirstColumn;

            foreach (var child in Children)
            {
                if (child.IsVisible)
                {
                    itemCount++;
                }
            }

            if (_rows == 0)
            {
                if (_columns == 0)
                {
                    _rows = _columns = (int)Math.Ceiling(Math.Sqrt(itemCount));
                }
                else
                {
                    _rows = Math.DivRem(itemCount, _columns, out int rem);

                    if (rem != 0)
                    {
                        _rows++;
                    }
                }
            }
            else if (_columns == 0)
            {
                _columns = Math.DivRem(itemCount, _rows, out int rem);

                if (rem != 0)
                {
                    _columns++;
                }
            }
        }
    }
}