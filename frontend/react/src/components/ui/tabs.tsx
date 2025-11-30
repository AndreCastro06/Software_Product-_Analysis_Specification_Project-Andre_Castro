import React, { useState } from "react";

interface TabProps {
  tabs: { id: string; label: string; content: React.ReactNode }[];
}

export const Tabs: React.FC<TabProps> = ({ tabs }) => {
  const [active, setActive] = useState(tabs[0].id);

  return (
    <div className="w-full">
      <div className="flex border-b mb-4">
        {tabs.map((tab) => (
          <button
            key={tab.id}
            onClick={() => setActive(tab.id)}
            className={`px-4 py-2 transition-colors border-b-2 ${
              active === tab.id
                ? "border-blue-600 text-blue-600 font-semibold"
                : "border-transparent text-gray-600 hover:text-gray-800"
            }`}
          >
            {tab.label}
          </button>
        ))}
      </div>

      <div>
        {tabs.map(
          (tab) =>
            active === tab.id && (
              <div key={tab.id}>{tab.content}</div>
            )
        )}
      </div>
    </div>
  );
};