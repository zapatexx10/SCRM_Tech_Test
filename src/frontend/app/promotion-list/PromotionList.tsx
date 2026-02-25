import {useEffect, useState} from 'react'
import './PromotionList.css'
import type {Promotion} from "../models/Promotion.tsx";
import type {PromotionResponse} from "../models/PromotionResponse.tsx";

export function PromotionList() {
    const [promotionList, setPromotionList] = useState<Promotion[]>([])
    const [loading, setLoading] = useState(false)

    const fetchPromotions = async () => {
        setLoading(true)

        try {
            const response = await fetch('/api/v1/DE/promotions?languageCode=DE')

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`)
            }

            const data: PromotionResponse = await response.json()
            setPromotionList(data.promotions)
        } catch (err) {
            console.error('Error fetching promotions:', err)
        } finally {
            setLoading(false)
        }
    }

    useEffect(() => {
        fetchPromotions()
    }, [])

    return (
        <main className="min-h-screen p-4 md:p-8 bg-linear-to-tr from-[#0000ff] to-[#ffff00]">
            <section className="max-w-5xl mx-auto" aria-labelledby="promotion-heading">
                <div className="bg-white rounded-xl shadow-sm border border-gray-200 overflow-hidden">

                    {/* Section Header */}
                    <div className="px-6 py-4 border-b border-gray-100 bg-gray-50/50">
                        <h2 id="promotion-heading" className="text-2xl text-center font-bold text-gray-800">
                            Promotions
                        </h2>
                    </div>

                    <div className="p-6">
                        {/* Loading Skeleton */}
                        {loading && promotionList.length === 0 && (
                            <div className="space-y-4" role="status" aria-live="polite">
                                {[...Array(5)].map((_, i) => (
                                    <div key={i} className="flex gap-4 animate-pulse">
                                        <div className="w-32 h-24 bg-gray-200 rounded-lg shrink-0"/>
                                        <div className="flex-1 space-y-3 py-1">
                                            <div className="h-4 bg-gray-200 rounded w-1/3"/>
                                            <div className="h-3 bg-gray-200 rounded w-full"/>
                                            <div className="h-3 bg-gray-200 rounded w-2/3"/>
                                        </div>
                                    </div>
                                ))}
                            </div>
                        )}

                        {/* Promotion Grid/List */}
                        {promotionList.length > 0 && (
                            <div className="grid grid-cols-1 gap-6">
                                {promotionList.map((promotion) => (
                                    <article
                                        key={promotion.promotionId}
                                        className="flex flex-col sm:flex-row border sm:h-48 border-gray-200 rounded-lg overflow-hidden hover:shadow-md transition-shadow duration-200 bg-white"
                                        aria-label={`Promotion ${promotion.texts.title}`}
                                    >
                                        {/* Image Side */}
                                        <div
                                            className="w-full h-48 sm:h-auto sm:w-32 md:w-48 shrink-0 bg-cover bg-center bg-no-repeat"
                                            style={{backgroundImage: `url(${promotion.images[0]})`}}
                                        />

                                        {/* Content Side */}
                                        <div className="flex flex-col p-4">
                                            <h3 className="text-lg font-semibold text-blue-600 mb-2 leading-tight">
                                                {promotion.texts.title}
                                            </h3>
                                            <p className="text-gray-600 text-sm line-clamp-1 mb-2">
                                                {promotion.texts.description}
                                            </p>
                                            <div className="text-2xl md:text-3xl font-bold text-gray-900 leading-none">
                                                {promotion.texts.discountTitle}
                                            </div>
                                            <footer className="mt-auto pt-3">
                                                <div className="text-sm font-medium text-green-700 bg-green-50 px-2 py-1 rounded-md inline-block">
                                                    {promotion.texts.discountDescription}
                                                </div>
                                            </footer>
                                        </div>
                                    </article>
                                ))}
                            </div>
                        )}
                    </div>
                </div>
            </section>
        </main>
    )
}

export default PromotionList
