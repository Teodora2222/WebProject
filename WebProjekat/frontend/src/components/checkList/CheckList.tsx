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
       className="px-5 py-2 rounded-lg text-sm font-medium
bg-green-500/20 text-green-300
border border-green-400/30
hover:bg-green-500/30 hover:border-green-300
transition">
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
              className="opacity-0 group-hover:opacity-100 p-2 bg-red-500/20 hover:bg-red-500 rounded-xl text-white transition-all flex-shrink-0"
             >
              <svg xmlns="http://www.w3.org/2000/svg" className="h-4 w-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16" />
              </svg>
            </button>
          </div>
        ))}
      </div>
    )}
  </div>
);
}