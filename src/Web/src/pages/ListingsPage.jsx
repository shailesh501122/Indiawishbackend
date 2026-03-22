import { PropertyCard } from '../components/PropertyCard';
const properties = new Array(6).fill(null).map((_, index) => ({ title: `Curated Listing ${index + 1}`, city: index % 2 === 0 ? 'Bengaluru' : 'Pune', area: index % 2 === 0 ? 'Whitefield' : 'Baner', bhk: 2 + (index % 3), price: 35000 + index * 5000, featured: index === 0, imageUrl: 'https://images.unsplash.com/photo-1494526585095-c41746248156?auto=format&fit=crop&w=900&q=80' }));
export const ListingsPage = () => (
  <main className="mx-auto max-w-7xl px-6 py-12">
    <div className="grid gap-6 lg:grid-cols-[280px_1fr]">
      <aside className="rounded-2xl bg-white p-6 shadow-sm ring-1 ring-slate-200"><h2 className="text-lg font-semibold">Filters</h2><div className="mt-4 space-y-4"><input className="w-full rounded-xl border border-slate-200 px-4 py-3" placeholder="City" /><input className="w-full rounded-xl border border-slate-200 px-4 py-3" placeholder="Max budget" /><select className="w-full rounded-xl border border-slate-200 px-4 py-3"><option>BHK</option></select><select className="w-full rounded-xl border border-slate-200 px-4 py-3"><option>Listing type</option></select></div></aside>
      <section><div className="mb-6 flex items-center justify-between"><h1 className="text-3xl font-bold">SEO-friendly listing catalogue</h1><select className="rounded-xl border border-slate-200 px-4 py-3"><option>Most recent</option><option>Price low to high</option></select></div><div className="grid gap-6 md:grid-cols-2 xl:grid-cols-3">{properties.map((property) => <PropertyCard key={property.title} property={property} />)}</div></section>
    </div>
  </main>
);
