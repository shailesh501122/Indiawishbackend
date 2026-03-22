const cards = [{ label: 'Active Listings', value: '12' }, { label: 'Visits Scheduled', value: '08' }, { label: 'Subscription Plan', value: 'Premium' }];
export const DashboardPage = () => (
  <main className="mx-auto max-w-7xl px-6 py-12"><h1 className="text-3xl font-bold">Owner & Tenant Dashboard</h1><div className="mt-8 grid gap-6 md:grid-cols-3">{cards.map((card) => <section key={card.label} className="rounded-2xl bg-white p-6 shadow-sm ring-1 ring-slate-200"><p className="text-sm text-slate-500">{card.label}</p><p className="mt-3 text-4xl font-bold">{card.value}</p></section>)}</div></main>
);
