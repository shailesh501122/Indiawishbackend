import { PropertyCard } from '../components/PropertyCard';
const featuredProperties = [
  { title: 'Skyline Residency 2BHK', city: 'Bengaluru', area: 'Whitefield', bhk: 2, price: 42000, featured: true, imageUrl: 'https://images.unsplash.com/photo-1560185007-cde436f6a4d0?auto=format&fit=crop&w=900&q=80' },
  { title: 'Palm Meadows Villa', city: 'Hyderabad', area: 'Gachibowli', bhk: 4, price: 18500000, featured: false, imageUrl: 'https://images.unsplash.com/photo-1600585154526-990dced4db0d?auto=format&fit=crop&w=900&q=80' }
];
export const HomePage = () => (
  <main className="mx-auto max-w-7xl px-6 py-12">
    <section className="grid gap-8 rounded-3xl bg-slate-900 px-8 py-16 text-white md:grid-cols-2">
      <div>
        <p className="mb-4 inline-flex rounded-full bg-white/10 px-4 py-2 text-sm">IndiaWish Real Estate Platform</p>
        <h1 className="text-5xl font-bold leading-tight">Rent, buy, and sell properties with subscription-powered visibility.</h1>
        <p className="mt-4 text-lg text-slate-200">Search verified homes, manage listings, schedule visits, and monetize with premium plans in one platform.</p>
      </div>
      <div className="rounded-3xl bg-white p-6 text-slate-900 shadow-2xl">
        <h2 className="text-2xl font-semibold">Start your search</h2>
        <div className="mt-6 grid gap-4 md:grid-cols-2">
          <input className="rounded-xl border border-slate-200 px-4 py-3" placeholder="City" />
          <input className="rounded-xl border border-slate-200 px-4 py-3" placeholder="Budget" />
          <select className="rounded-xl border border-slate-200 px-4 py-3"><option>Property type</option></select>
          <button className="rounded-xl bg-slate-900 px-4 py-3 font-semibold text-white">Search listings</button>
        </div>
      </div>
    </section>
    <section className="mt-12">
      <div className="mb-6 flex items-center justify-between"><h2 className="text-2xl font-semibold">Featured properties</h2><a href="/listings" className="text-sm font-semibold text-slate-700">Browse all listings →</a></div>
      <div className="grid gap-6 md:grid-cols-2 xl:grid-cols-3">{featuredProperties.map((property) => <PropertyCard key={property.title} property={property} />)}</div>
    </section>
  </main>
);
