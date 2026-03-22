export const PropertyCard = ({ property }) => (
  <article className="rounded-2xl bg-white p-5 shadow-sm ring-1 ring-slate-200">
    <img src={property.imageUrl} alt={property.title} className="mb-4 h-52 w-full rounded-xl object-cover" />
    <div className="flex items-center justify-between">
      <h3 className="text-lg font-semibold">{property.title}</h3>
      {property.featured && <span className="rounded-full bg-amber-100 px-3 py-1 text-xs font-semibold text-amber-700">Featured</span>}
    </div>
    <p className="mt-2 text-sm text-slate-500">{property.city} · {property.area} · {property.bhk} BHK</p>
    <p className="mt-4 text-xl font-bold text-slate-900">₹{property.price.toLocaleString('en-IN')}</p>
  </article>
);
