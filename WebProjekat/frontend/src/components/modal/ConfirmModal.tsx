interface Props {
  title: string;
  message: string;
  onConfirm: () => void;
  onCancel: () => void;
}

export function ConfirmModal({
  title,
  message,
  onConfirm,
  onCancel,
}: Props) {
  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/60 backdrop-blur-md">

      <div
        className="
          relative
          w-full max-w-md mx-4
          overflow-hidden
          rounded-3xl
          border border-green-500/20
          bg-gradient-to-br
          from-[#052e2b]
          via-[#0b3b35]
          to-[#081f1c]
          shadow-[0_0_40px_rgba(34,197,94,0.12)]
        "
      >
        <div className="absolute inset-0 bg-white/[0.02]" />

        <div className="relative p-8">

          <div className="mb-5 h-1 w-20 rounded-full bg-green-400/70 mx-auto" />

          <div className="text-center">
            <h2 className="text-2xl font-bold text-white">
              {title}
            </h2>

            <p className="mt-3 text-white/60 leading-relaxed">
              {message}
            </p>
          </div>

          <div className="flex gap-3 mt-8">
            <button
              onClick={onCancel}
              className="
                flex-1 py-3 rounded-xl
                border border-white/10
                bg-white/5
                text-white/70
                hover:bg-white/10
                hover:text-white
                transition-all
              "
            >
              Cancel
            </button>

            <button
              onClick={onConfirm}
              className="
                flex-1 py-3 rounded-xl
                bg-green-500/20
                border border-green-400/30
                text-green-300
                hover:bg-green-500/30
                hover:border-green-300
                transition-all
                font-semibold
              "
            >
              Confirm
            </button>
          </div>

        </div>
      </div>
    </div>
  );
}