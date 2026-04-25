import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import toast from "react-hot-toast";
import type { CheckListItemDto } from "../../models/checkListItem/CheckListItemDto";
import type { ICheckListItemAPi } from "../../api/checkListItem/ICheckListItemApi";

interface Props {
  checkListApi: ICheckListItemAPi;
}

export function CheckList({ checkListApi }: Props) {
  const { id: travelPlanId } = useParams();

  const [items, setItems] = useState<CheckListItemDto[]>([]);
  const [newItem, setNewItem] = useState("");
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetchItems();
  }, []);

  const fetchItems = async () => {
    try {
      const data = await checkListApi.getAllCheckLists(Number(travelPlanId));
      setItems(data);
    } catch {
      toast.error("Failed to load checklist.");
    } finally {
      setLoading(false);
    }
  };

  const handleAdd = async () => {
    if (!newItem.trim()) return;

    try {
      const created = await checkListApi.createCheckList(Number(travelPlanId), {
        name: newItem,
      });

      setItems((prev) => [...prev, created]);
      setNewItem("");
    } catch {
      toast.error("Failed to add item.");
    }
  };

   const handleToggle = async (item: CheckListItemDto) => {
    try {
      await checkListApi.toggleCheckList(
        item.id,
        { isCompleted: !item.isCompleted },
        Number(travelPlanId),
      );

      setItems((prev) =>
        prev.map((i) =>
          i.id === item.id
            ? { ...i, isCompleted: !i.isCompleted }
            : i
        )
      );
    } catch {
      toast.error("Failed to update item.");
    }
  };


  const handleDelete = async (id: number) => {
    try {
      await checkListApi.deleteCheckList(Number(travelPlanId), id);
      setItems((prev) => prev.filter((i) => i.id !== id));
    } catch {
      toast.error("Failed to delete.");
    }
  };

  if (loading) {
    return (
      <div className="flex justify-center py-16">
        <div className="animate-spin h-8 w-8 border-b-2 border-green-500 rounded-full"></div>
      </div>
    );
  }

return (
  <div className="w-full">

    <div className="flex items-end justify-between mb-6 pb-4 border-b border-white/10">
      <div>
        <h2 className="text-xl font-bold text-white">Checklist</h2>
        <p className="text-white/50 text-sm">
          {items.filter(i => i.isCompleted).length} / {items.length} completed
        </p>
      </div>

      <button
        onClick={handleAdd}
        className="bg-green-500 hover:bg-green-600 text-white px-4 py-2 rounded-xl font-bold transition-all hover:scale-105 shadow-lg shadow-green-500/20 text-sm"
        >
        + Add Item
      </button>
    </div>

    <div className="mb-6">
      <input
        value={newItem}
        onChange={(e) => setNewItem(e.target.value)}
        placeholder="Add item..."
        className="w-full px-4 py-3 rounded-xl bg-white/5 border border-white/10 text-white placeholder-white/30 outline-none focus:ring-1 focus:ring-green-400"
      />
    </div>

    {items.length === 0 ? (
      <div className="text-center py-16 bg-white/5 rounded-3xl border border-dashed border-white/20 text-white/40">
        No checklist items yet.
      </div>
    ) : (
      <div className="flex flex-col gap-3">

        {items.map((item) => (
          <div
            key={item.id}
            className={`group flex items-center justify-between bg-white/5 border rounded-xl px-4 py-3 transition
              ${item.isCompleted 
                ? "border-green-500/30 bg-green-500/10" 
                : "border-white/10 hover:border-green-400/30"}
            `}
          >
            <div className="flex items-center gap-3">

              <input
                type="checkbox"
                checked={item.isCompleted}
                onChange={() => handleToggle(item)}
                className="w-5 h-5 accent-green-500 cursor-pointer"
              />

              <span
                className={`text-sm ${
                  item.isCompleted
                    ? "line-through text-white/40"
                    : "text-white"
                }`}
              >
                {item.name}
              </span>
            </div>

            <button
              onClick={() => handleDelete(item.id)}
              className="opacity-0 group-hover:opacity-100 text-red-400 hover:text-red-300 transition text-sm"
            >
              Delete
            </button>
          </div>
        ))}
      </div>
    )}
  </div>
);
}