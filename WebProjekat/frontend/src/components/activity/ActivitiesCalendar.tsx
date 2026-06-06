import { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import type { ActivityDto } from "../../models/activity/ActivityDto";
import { Status } from "../../enums/Status";

interface Props {
  activities: ActivityDto[];
}

const statusColor: Record<Status, string> = {
  [Status.PLANNED]: "bg-blue-500/20 text-blue-400 border-blue-500/30",
  [Status.RESERVED]: "bg-yellow-500/20 text-yellow-400 border-yellow-500/30",
  [Status.FINISHED]: "bg-green-500/20 text-green-400 border-green-500/30",
  [Status.CANCELLED]: "bg-red-500/20 text-red-400 border-red-500/30",
};

const statusDot: Record<Status, string> = {
  [Status.PLANNED]: "bg-blue-400",
  [Status.RESERVED]: "bg-yellow-400",
  [Status.FINISHED]: "bg-green-400",
  [Status.CANCELLED]: "bg-red-400",
};

const statusBorder: Record<Status, string> = {
  [Status.PLANNED]: "border-l-blue-400",
  [Status.RESERVED]: "border-l-yellow-400",
  [Status.FINISHED]: "border-l-green-400",
  [Status.CANCELLED]: "border-l-red-400",
};

export function ActivitiesCalendar({ activities }: Props) {
  const { id: travelPlanId } = useParams();
  const navigate = useNavigate();

  const today = new Date();
  const [currentMonth, setCurrentMonth] = useState(() => {
    if (activities.length > 0) {
      const sorted = [...activities].sort((a, b) => a.date.localeCompare(b.date));
      const first = new Date(sorted[0].date);
      return new Date(first.getFullYear(), first.getMonth(), 1);
    }
    return new Date(today.getFullYear(), today.getMonth(), 1);
  });

  const [selectedDate, setSelectedDate] = useState<string | null>(null);

  const year = currentMonth.getFullYear();
  const month = currentMonth.getMonth();

  const firstDay = new Date(year, month, 1).getDay();
  const startOffset = firstDay === 0 ? 6 : firstDay - 1;
  const daysInMonth = new Date(year, month + 1, 0).getDate();

  const activityMap = activities.reduce((acc, act) => {
    const key = act.date.substring(0, 10);
    if (!acc[key]) acc[key] = [];
    acc[key].push(act);
    return acc;
  }, {} as Record<string, ActivityDto[]>);

  const prevMonth = () => setCurrentMonth(new Date(year, month - 1, 1));
  const nextMonth = () => setCurrentMonth(new Date(year, month + 1, 1));

  const monthName = currentMonth.toLocaleDateString("en-GB", {
    month: "long", year: "numeric",
  });

  const selectedActivities = selectedDate ? (activityMap[selectedDate] ?? []) : [];

  const cells: (number | null)[] = [
    ...Array(startOffset).fill(null),
    ...Array.from({ length: daysInMonth }, (_, i) => i + 1),
  ];
  while (cells.length % 7 !== 0) cells.push(null);

  const toKey = (day: number) => {
    const m = String(month + 1).padStart(2, "0");
    const d = String(day).padStart(2, "0");
    return `${year}-${m}-${d}`;
  };

  const isToday = (day: number) =>
    today.getDate() === day &&
    today.getMonth() === month &&
    today.getFullYear() === year;

  return (
    <div className="w-full flex flex-col gap-6">
      <div className="flex items-center justify-between bg-white/5 border border-white/10 rounded-2xl px-6 py-4">
        <button
          onClick={prevMonth}
          className="w-9 h-9 rounded-xl bg-white/5 hover:bg-white/15 text-white/60 hover:text-white transition flex items-center justify-center text-xl font-light"
        >
          ‹
        </button>
        <div className="text-center">
          <h3 className="text-white font-bold text-xl tracking-wide">{monthName}</h3>
          <p className="text-white/30 text-xs mt-0.5">
            {Object.keys(activityMap).filter(k => k.startsWith(`${year}-${String(month+1).padStart(2,"0")}`)).length} days with activities
          </p>
        </div>
        <button
          onClick={nextMonth}
          className="w-9 h-9 rounded-xl bg-white/5 hover:bg-white/15 text-white/60 hover:text-white transition flex items-center justify-center text-xl font-light"
        >
          ›
        </button>
      </div>

      <div className="grid grid-cols-7 text-center">
        {["Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"].map((d, i) => (
          <div
            key={d}
            className={`py-2 text-xs font-bold uppercase tracking-widest ${
              i >= 5 ? "text-green-400/60" : "text-white/30"
            }`}
          >
            {d}
          </div>
        ))}
      </div>

      <div className="grid grid-cols-7 gap-2">
        {cells.map((day, idx) => {
          if (!day) return <div key={`empty-${idx}`} className="min-h-[80px]" />;

          const key = toKey(day);
          const dayActivities = activityMap[key] ?? [];
          const hasActivities = dayActivities.length > 0;
          const isSelected = selectedDate === key;
          const todayDay = isToday(day);
          const isWeekend = (idx % 7) >= 5;

          return (
            <button
              key={key}
              onClick={() => setSelectedDate(isSelected ? null : key)}
              className={`
                relative flex flex-col items-start
                rounded-xl p-2 min-h-[80px]
                transition-all duration-200 text-left
                ${isSelected
                  ? "bg-green-500/20 border-2 border-green-400/60 shadow-[0_0_20px_rgba(34,197,94,0.2)]"
                  : hasActivities
                  ? "bg-white/8 border border-white/15 hover:bg-white/12 hover:border-green-400/40 hover:shadow-[0_0_12px_rgba(34,197,94,0.1)]"
                  : isWeekend
                  ? "bg-white/3 border border-white/5 hover:bg-white/6"
                  : "border border-transparent hover:bg-white/5"
                }
              `}
            >
              <span
                className={`
                  text-sm font-bold w-7 h-7 flex items-center justify-center rounded-lg mb-1 flex-shrink-0
                  ${todayDay
                    ? "bg-green-500 text-white shadow-[0_0_10px_rgba(34,197,94,0.5)]"
                    : isSelected
                    ? "text-green-300"
                    : isWeekend
                    ? "text-white/50"
                    : "text-white/70"
                  }
                `}
              >
                {day}
              </span>

              {hasActivities && (
                <div className="flex flex-col gap-0.5 w-full">
                  {dayActivities.slice(0, 2).map((act) => (
                    <div
                      key={act.id}
                      className={`
                        w-full text-[10px] px-1.5 py-0.5 rounded-md
                        border-l-2 bg-white/10 text-white/70 truncate leading-tight
                        ${act.status ? statusBorder[act.status] : "border-l-white/30"}
                      `}
                    >
                      {act.time && <span className="opacity-60">{act.time.substring(0,5)} </span>}
                      {act.name}
                    </div>
                  ))}
                  {dayActivities.length > 2 && (
                    <div className="text-[10px] text-white/40 pl-1">
                      +{dayActivities.length - 2} more
                    </div>
                  )}
                </div>
              )}
            </button>
          );
        })}
      </div>

      <div className="flex gap-4 flex-wrap text-xs text-white/40 px-1">
        {Object.entries(statusDot).map(([status, dot]) => (
          <span key={status} className="flex items-center gap-1.5">
            <span className={`w-2 h-2 rounded-full ${dot}`} />
            {status}
          </span>
        ))}
      </div>

      {selectedDate && (
        <div className="mt-2 bg-white/3 border border-white/10 rounded-2xl p-5">
          <div className="flex items-center justify-between mb-4">
            <p className="text-white font-semibold">
              {new Date(selectedDate + "T00:00:00").toLocaleDateString("en-GB", {
                weekday: "long", day: "numeric", month: "long",
              })}
            </p>
            <button
              onClick={() => setSelectedDate(null)}
              className="text-white/30 hover:text-white transition text-lg"
            >
              ×
            </button>
          </div>

          {selectedActivities.length === 0 ? (
            <div className="text-center py-8 border border-dashed border-white/10 rounded-xl text-white/30 text-sm">
              No activities on this day
            </div>
          ) : (
            <div className="flex flex-col gap-3">
              {selectedActivities
                .sort((a, b) => (a.time ?? "").localeCompare(b.time ?? ""))
                .map((act) => (
                  <div
                    key={act.id}
                    onClick={() => navigate(`/trips/${travelPlanId}/activities/${act.id}/edit`)}
                    className={`
                      group cursor-pointer
                      bg-[#064e3b]/30 border border-white/10
                      border-l-4 ${act.status ? statusBorder[act.status] : "border-l-white/20"}
                      rounded-xl p-4
                      hover:border-green-500/50 hover:bg-[#064e3b]/50
                      transition-all duration-200
                      flex items-center gap-4
                    `}
                  >
                    <div className="flex-1 min-w-0">
                      <div className="flex items-center gap-2 flex-wrap">
                        <span className="text-white font-semibold group-hover:text-green-400 transition-colors">
                          {act.name}
                        </span>
                        {act.status && (
                          <span className={`text-xs px-2 py-0.5 rounded-lg border font-medium ${statusColor[act.status]}`}>
                            {act.status}
                          </span>
                        )}
                      </div>
                      <div className="flex gap-3 mt-1 text-xs text-white/40 flex-wrap">
                        {act.time && <span>🕐 {act.time}</span>}
                        {act.location && <span>📍 {act.location}</span>}
                        {act.estimatedCost && <span>💰 €{act.estimatedCost}</span>}
                      </div>
                      {act.description && (
                        <p className="text-white/30 text-sm mt-1.5 line-clamp-2">{act.description}</p>
                      )}
                    </div>
                    <svg xmlns="http://www.w3.org/2000/svg" className="h-4 w-4 text-white/20 group-hover:text-green-400 transition flex-shrink-0" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                      <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M9 5l7 7-7 7" />
                    </svg>
                  </div>
                ))}
            </div>
          )}
        </div>
      )}
    </div>
  );
}