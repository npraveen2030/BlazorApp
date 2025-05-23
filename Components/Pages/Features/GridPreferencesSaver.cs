namespace BlazorApp.Components.Pages.Features
{
    public class GridPreferencesSaver<T>
    {
        public Dictionary<string, GridMeta> GridState { get; set; }
        public List<string> VisibleColumns { get; set; } = new();

        public GridPreferencesSaver(Dictionary<string, GridMeta> gridState)
        {
            GridState = gridState;
        }

        public void OnColumnResized(DataGridColumnResizedEventArgs<T> args)
        {
            var columnName = args.Column.Property;
            var newWidth = args.Width;

            Console.WriteLine($"Resized: {columnName} to {newWidth}");

            if (GridState.ContainsKey(columnName))
            {
                GridState[columnName].Width = newWidth.ToString()+"px";
            }
        }

        public void OnColumnReordered(DataGridColumnReorderedEventArgs<T> args)
        {
            var oldIndex = args.OldIndex;
            var newIndex = args.NewIndex;
            var property = args.Column?.Property;

            if (property is null || !GridState.ContainsKey(property))
                return;

            var ordered = GridState.OrderBy(kvp => kvp.Value.OrderIndex).ToList();

            var movingItem = ordered.FirstOrDefault(kvp => kvp.Key == property);
            if (movingItem.Key is null) return;

            ordered.Remove(movingItem);
            ordered.Insert(newIndex, movingItem);

            for (int i = 0; i < ordered.Count; i++)
            {
                ordered[i].Value.OrderIndex = i;
            }
        }

        public void PickedColumnsChanged(DataGridPickedColumnsChangedEventArgs<T> args)
        {

            foreach (var column in args.Columns)
            {
                VisibleColumns.Add(column.Property);
            }

            foreach (var key in GridState.Keys)
            {
                if (VisibleColumns.Contains(key))
                {
                    GridState[key].Visible = true;
                }
                else
                {
                    GridState[key].Visible = false;
                }
            }

            VisibleColumns = new();
        }
    }
}
