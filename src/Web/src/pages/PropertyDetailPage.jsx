export const PropertyDetailPage = () => (
  <main className="mx-auto max-w-6xl px-6 py-12">
    <div className="grid gap-10 lg:grid-cols-[1.4fr_0.6fr]">
      <section>
        <img src="https://images.unsplash.com/photo-1505693416388-ac5ce068fe85?auto=format&fit=crop&w=1200&q=80" alt="Property" className="h-[420px] w-full rounded-3xl object-cover" />
        <h1 className="mt-8 text-4xl font-bold">Skyline Residency 2BHK</h1>
        <p className="mt-3 text-slate-600">Whitefield, Bengaluru · 2 BHK · Semi-furnished</p>
        <p className="mt-6 leading-8 text-slate-700">Premium apartment with power backup, parking, gym, and easy access to metro, schools, and tech parks.</p>
        <div className="mt-8 flex flex-wrap gap-3">{['Parking', 'Lift', 'Gym', 'Power Backup'].map((amenity) => <span key={amenity} className="rounded-full bg-slate-100 px-4 py-2 text-sm">{amenity}</span>)}</div>
      </section>
      <aside className="rounded-3xl bg-white p-6 shadow-sm ring-1 ring-slate-200"><p className="text-sm uppercase tracking-[0.2em] text-slate-500">Schedule a visit</p><p className="mt-3 text-3xl font-bold">₹42,000 / month</p><div className="mt-6 space-y-4"><input className="w-full rounded-xl border border-slate-200 px-4 py-3" placeholder="Preferred date" /><textarea className="w-full rounded-xl border border-slate-200 px-4 py-3" rows="5" placeholder="Message to owner" /><button className="w-full rounded-xl bg-slate-900 px-4 py-3 font-semibold text-white">Request visit</button></div></aside>
    </div>
  </main>
);
