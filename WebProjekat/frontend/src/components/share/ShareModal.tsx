import { useState } from "react";
import { QRCodeSVG } from "qrcode.react";
import type { IShareApi } from "../../api/share/IShareApi";
 
interface Props {
  travelPlanId: number;
  shareApi: IShareApi;
  onClose: () => void;
}
 
export function ShareModal({ travelPlanId, shareApi, onClose }: Props) {
  const [permission, setPermission] = useState<"VIEW" | "EDIT">("VIEW");
  const [shareUrl, setShareUrl] = useState<string | null>(null);
  const [loading, setLoading] = useState(false);
  const [copied, setCopied] = useState(false);
 
  const handleShare = async () => {
    setLoading(true);
    try {
      const result = await shareApi.createShare(travelPlanId, { permission });
      setShareUrl(result.url);
    } catch {
      alert("Failed to generate share link.");
    } finally {
      setLoading(false);
    }
  };
 
  const handleCopy = () => {
    if (!shareUrl) return;
    navigator.clipboard.writeText(shareUrl);
    setCopied(true);
    setTimeout(() => setCopied(false), 2000);
  };
 
  return (
    <div className="fixed inset-0 bg-black/70 backdrop-blur-sm flex items-center justify-center z-50">
      <div className="bg-[#0d2818] border border-white/10 rounded-3xl p-8 w-full max-w-md shadow-2xl">
 
        <div className="flex items-center justify-between mb-6">
          <h2 className="text-white font-bold text-xl">Share Trip</h2>
          <button
            onClick={onClose}
            className="text-white/40 hover:text-white transition text-xl"
          >
            ✕
          </button>
        </div>
 
        {!shareUrl ? (
          <>
            <p className="text-white/50 text-sm mb-4">Choose access level:</p>
            <div className="grid grid-cols-2 gap-3 mb-6">
              <button
                onClick={() => setPermission("VIEW")}
                className={`p-4 rounded-xl border text-left transition ${
                  permission === "VIEW"
                    ? "border-green-400/50 bg-green-500/10 text-green-300"
                    : "border-white/10 text-white/50 hover:border-white/20"
                }`}
              >
                <p className="font-semibold"> View only</p>
                <p className="text-xs mt-1 opacity-60">Can see the plan</p>
              </button>
              <button
                onClick={() => setPermission("EDIT")}
                className={`p-4 rounded-xl border text-left transition ${
                  permission === "EDIT"
                    ? "border-blue-400/50 bg-blue-500/10 text-blue-300"
                    : "border-white/10 text-white/50 hover:border-white/20"
                }`}
              >
                <p className="font-semibold"> Can edit</p>
                <p className="text-xs mt-1 opacity-60">Can modify the plan</p>
              </button>
            </div>
 
            <button
              onClick={handleShare}
              disabled={loading}
              className="w-full py-3 rounded-xl bg-gradient-to-r from-emerald-400 to-green-500 text-white font-semibold transition hover:scale-105 disabled:opacity-60"
            >
              {loading ? "Generating..." : "Generate QR Code"}
            </button>
          </>
        ) : (
          <>
            <div className="flex flex-col items-center gap-4">
              <div className="bg-white p-4 rounded-2xl">
                <QRCodeSVG value={shareUrl} size={180} />
              </div>
 
              <p className="text-white/40 text-xs text-center">
                Scan to {permission === "VIEW" ? "view" : "edit"} this trip
              </p>
 
              <div className="w-full bg-white/5 border border-white/10 rounded-xl px-4 py-3 flex items-center gap-3">
                <p className="text-white/50 text-xs truncate flex-1">{shareUrl}</p>
                <button
                  onClick={handleCopy}
                  className="text-green-400 text-xs font-semibold shrink-0"
                >
                  {copied ? "Copied!" : "Copy"}
                </button>
              </div>
 
              <button
                onClick={() => setShareUrl(null)}
                className="text-white/40 hover:text-white text-sm transition"
              >
                ← Generate new link
              </button>
            </div>
          </>
        )}
      </div>
    </div>
  );
}
 