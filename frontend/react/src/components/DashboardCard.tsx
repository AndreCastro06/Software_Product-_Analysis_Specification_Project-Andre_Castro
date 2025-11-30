import { Link } from "react-router-dom";

interface DashboardCardProps {
  title: string;
  description: string;
  icon: React.ReactNode;
  to: string;
}

export default function DashboardCard({
  title,
  description,
  icon,
  to,
}: DashboardCardProps) {
  return (
    <Link to={to} className="block">
      <div
        className="
          bg-orange-200
          border border-black/20
          rounded-xl
          p-6
          shadow-md

          transition-all duration-300
          hover:bg-green-300
          hover:scale-105
          cursor-pointer
        "
      >
        <div className="flex items-center gap-2 text-lg font-semibold">
          {icon}
          {title}
        </div>

        <p className="text-sm text-gray-700 mt-1">
          {description}
        </p>
      </div>
    </Link>
  );
}
