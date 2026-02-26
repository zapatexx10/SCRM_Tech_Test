import { useEffect, useState } from "react"
import { useNavigate, useParams, useSearchParams } from "react-router"
import type { Promotion } from "~/models/Promotion"
import type { PromotionDetailResponse } from "~/models/PromotionResponse"

export default function PromotionDetail() {
    const { promotionId } = useParams<{ promotionId: string }>()
    const navigate = useNavigate()
    const [searchParams] = useSearchParams()
    
    const [promotion, setPromotion] = useState<Promotion | null>(null)
    const [loading, setLoading] = useState(true)
    const [error, setError] = useState<string | null>(null)
    const country = searchParams.get('country') || 'DE'
    const lang = searchParams.get('lang') || 'DE'

    useEffect(() => {
        const fetchPromotionDetail = async () => {
            if (!promotionId) return

            setLoading(true)
            setError(null)

            try {                
                const response = await fetch(
                    `http://localhost:54679/api/v1/${country}/promotions/${promotionId}?languageCode=${lang}`
                )

                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`)
                }

                const data: PromotionDetailResponse = await response.json()

                setPromotion(data.promotion)
            } catch (err) {
                console.error('Error fetching promotion:', err)
                setError(err instanceof Error ? err.message : 'Unknown error')
            } finally {
                setLoading(false)
            }
        }

        fetchPromotionDetail()
    }, [promotionId])

    if (loading) {
        return (
            <main className="min-h-screen p-4 md:p-8 bg-gradient-to-tr from-[#0000ff] to-[#ffff00]">
                <div className="max-w-4xl mx-auto">
                    <div className="bg-white rounded-xl shadow-sm border border-gray-200 p-6">
                        <div className="animate-pulse space-y-4">
                            <div className="h-8 bg-gray-200 rounded w-1/3"/>
                            <div className="h-64 bg-gray-200 rounded"/>
                            <div className="h-4 bg-gray-200 rounded"/>
                            <div className="h-4 bg-gray-200 rounded w-5/6"/>
                        </div>
                    </div>
                </div>
            </main>
        )
    }

    if (error || !promotion) {
        return (
            <main className="min-h-screen p-4 md:p-8 bg-gradient-to-tr from-[#0000ff] to-[#ffff00]">
                <div className="max-w-4xl mx-auto">
                    <div className="bg-white rounded-xl shadow-sm border border-gray-200 p-6">
                        <h2 className="text-xl font-semibold text-red-600 mb-4">Error</h2>
                        <p className="text-gray-600 mb-4">{error || 'Promotion not found'}</p>
                        <button
                            onClick={() => navigate('/')}
                            className="px-4 py-2 bg-blue-600 text-white rounded-lg hover:bg-blue-700 transition-colors"
                        >
                            Back to Promotions
                        </button>
                    </div>
                </div>
            </main>
        )
    }

    return (
        <main className="min-h-screen p-4 md:p-8 bg-gradient-to-tr from-[#0000ff] to-[#ffff00]">
            <div className="max-w-4xl mx-auto">
                <div className="bg-white rounded-xl shadow-sm border border-gray-200 overflow-hidden">
                    
                    {/* Header with back button */}
                    <div className="px-6 py-4 border-b border-gray-100 bg-gray-50/50 flex items-center gap-4">
                        <button
                            onClick={() => navigate('/')}
                            className="text-gray-600 hover:text-gray-900 transition-colors"
                            aria-label="Back to promotions"
                        >
                            <svg className="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M10 19l-7-7m0 0l7-7m-7 7h18" />
                            </svg>
                        </button>
                        <h1 className="text-2xl font-bold text-gray-800">
                            Promotion Details
                        </h1>
                    </div>

                    {/* Content */}
                    <div className="p-6 space-y-6">
                        
                        {/* Image Gallery */}
                        {promotion.images && promotion.images.length > 0 && (
                            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                                {promotion.images.map((image, idx) => (
                                    <img
                                        key={idx}
                                        src={image}
                                        alt={`${promotion.texts.title} - Image ${idx + 1}`}
                                        className="w-full h-64 object-cover rounded-lg border border-gray-200"
                                    />
                                ))}
                            </div>
                        )}

                        {/* Title */}
                        <div>
                            <label className="block text-sm font-medium text-gray-700 mb-2">
                                Title
                            </label>
                            <div className="text-2xl font-bold text-gray-900">
                                {promotion.texts.title}
                            </div>
                        </div>

                        {/* Description */}
                        <div>
                            <label className="block text-sm font-medium text-gray-700 mb-2">
                                Description
                            </label>
                            <p className="text-gray-700 leading-relaxed">
                                {promotion.texts.description}
                            </p>
                        </div>

                        {/* Discount Main Info */}
                        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                            <div>
                                <label className="block text-sm font-medium text-gray-700 mb-2">
                                    Discount Title
                                </label>
                                <div className="text-3xl font-bold text-blue-600">
                                    {promotion.texts.discountTitle}
                                </div>
                            </div>
                            <div>
                                <label className="block text-sm font-medium text-gray-700 mb-2">
                                    Discount Description
                                </label>
                                <div className="text-lg font-medium text-green-700 bg-green-50 px-4 py-2 rounded-lg inline-block">
                                    {promotion.texts.discountDescription}
                                </div>
                            </div>
                        </div>
                        <br></br>
                        {/* Discounts Section */}
                        {promotion.discounts && promotion.discounts.length > 0 && (
                            <div>
                                <label className="block text-lg font-semibold text-gray-800 mb-4">
                                    Available Discounts ({promotion.discounts.length})
                                </label>
                                <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                                    {promotion.discounts.map((discount, index) => (
                                        <div
                                            key={index}
                                            className="border border-gray-200 rounded-lg p-5 bg-gradient-to-br from-blue-50 to-white hover:shadow-md transition-shadow"
                                        >
                                            {/* Type Badge */}
                                            <div className="flex items-center justify-between mb-3">
                                                <span className={`px-3 py-1 rounded-full text-xs font-semibold ${
                                                    discount.type === 'Store' 
                                                        ? 'bg-purple-100 text-purple-700'
                                                        : discount.type === 'Online'
                                                        ? 'bg-green-100 text-green-700'
                                                        : 'bg-gray-100 text-gray-700'
                                                }`}>
                                                    {discount.type}
                                                </span>
                                                {discount.hasPrice && (
                                                    <span className="text-xs text-green-600 font-medium">
                                                        ✓ Price Available
                                                    </span>
                                                )}
                                            </div>

                                            {/* Prices */}
                                            <div className="space-y-2 mb-4">
                                                {/* Original Price */}
                                                {discount.originalPrice !== discount.finalPrice && (
                                                    <div className="flex items-center gap-2">
                                                        <span className="text-sm text-gray-500">Original:</span>
                                                        <span className="text-lg text-gray-400 line-through">
                                                            €{discount.originalPrice.toFixed(2)}
                                                        </span>
                                                    </div>
                                                )}
                                                
                                                {/* Final Price*/}
                                                <div className="flex items-center gap-2">
                                                    <span className="text-sm text-gray-700 font-medium">Final Price:</span>
                                                    <span className="text-3xl font-bold text-blue-600">
                                                        €{discount.finalPrice.toFixed(2)}
                                                    </span>
                                                </div>

                                                {/* Savings calculation */}
                                                {discount.originalPrice > discount.finalPrice && (
                                                    <div className="text-sm text-green-700 font-medium">
                                                        Save €{(discount.originalPrice - discount.finalPrice).toFixed(2)} 
                                                        ({(((discount.originalPrice - discount.finalPrice) / discount.originalPrice) * 100).toFixed(0)}% off)
                                                    </div>
                                                )}
                                            </div>

                                            {/* Lowest Price Last 30 Days */}
                                            {discount.lowestPriceLast30Days > 0 && (
                                                <div className="mb-3 p-2 bg-yellow-50 rounded border border-yellow-200">
                                                    <p className="text-xs text-yellow-800">
                                                        <span className="font-semibold">Lowest in 30 days:</span> €{discount.lowestPriceLast30Days.toFixed(2)}
                                                    </p>
                                                </div>
                                            )}

                                            {/* Buy X Pay Y offer */}
                                            {discount.unitsToBuy > 0 && discount.unitsToPay > 0 && discount.unitsToBuy !== discount.unitsToPay && (
                                                <div className="flex items-center gap-2 p-3 bg-gradient-to-r from-red-50 to-orange-50 rounded-lg border border-red-200">
                                                    <div>
                                                        <p className="font-bold text-red-700">
                                                            Buy {discount.unitsToBuy}, Pay {discount.unitsToPay}!
                                                        </p>
                                                        <p className="text-xs text-red-600">
                                                            Get {discount.unitsToBuy - discount.unitsToPay} free
                                                        </p>
                                                    </div>
                                                </div>
                                            )}

                                            {/* Price Type */}
                                            {discount.priceType && (
                                                <div className="mt-3 text-xs text-gray-500">
                                                    <span className="font-medium">Price Type:</span> {discount.priceType}
                                                </div>
                                            )}
                                        </div>
                                    ))}
                                </div>
                            </div>
                        )}
                    </div>
                </div>
            </div>
        </main>
    )
}