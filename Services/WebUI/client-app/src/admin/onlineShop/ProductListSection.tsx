import CreateProductSection from './productCreation/ProductUpperSection'
import ProductList from './productList/ProductList'
import ProductUpperSection from "./productCreation/ProductUpperSection";
import {useStore} from "../../global/stores/store";
import {observer} from "mobx-react-lite";
import {useEffect, useState} from "react";
import {Product} from "../../global/models/product";

function ProductListSection() {
    const {adminProductStore} = useStore()


    async function handleSearch(phrase: string | null) {
        adminProductStore.setSearchPhrase(phrase)
        adminProductStore.loadProducts()
    }

    return (
        <>
            <ProductUpperSection onApplySearch={(phrase) => handleSearch(phrase)}  />
            <ProductList products={adminProductStore.products} loading={adminProductStore.loading} />
        </>
    )
}

export default observer(ProductListSection)