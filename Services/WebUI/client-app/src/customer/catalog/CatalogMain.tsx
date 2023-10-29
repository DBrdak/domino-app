import {useEffect} from 'react'
import NavBar from '../components/NavBar'
import { observer } from 'mobx-react-lite'
import { Grid } from '@mui/material'
import FilterPanel from './FilterPanel'
import ProductCatalog from './CatalogPanel'
import '../../global/styles/index.css'
import ShoppingCartBadge from './ShoppingCartBadge'
import { useStore } from '../../global/stores/store'
import { FilterOptions } from '../../global/models/filterOptions'
import CategorySelection from './CategorySelection'
import ResetCategoryButton from './ResetCategoryButton'
import { setPageTitle } from '../../global/utils/pageTitle'
import InfiniteScroll from "react-infinite-scroller";
import {PagingParams} from "../../global/models/pagination";
import LoadingComponent from "../../components/LoadingComponent";
import Footer from "../footer/Footer";

interface Props {
  category: string | null
}

function CatalogMain({category}: Props) {
    const {shoppingCartStore, productStore} = useStore()

    useEffect(() => {
        setPageTitle('Produkty')
        shoppingCartStore.loadShoppingCart()
    }, [])

    useEffect(() => {
        category && productStore.loadProducts(category)
    }, [category])

    function handleApplySearch(name: string | null): void {
        productStore.setFilter({...productStore.filterParams, searchPhrase: name})
        productStore.loadProducts(category!)
    }

    function handleApplyFilter(filterOptions: FilterOptions): void {
        productStore.setFilter(filterOptions)
        productStore.loadProducts(category!)
    }

    function handleGetNext() {
        productStore.setLoadingNext(true)
        productStore.setPagingParams(new PagingParams(productStore.pagination!.page + 1))
        category && productStore.loadProducts(category).then(() => productStore.setLoadingNext(false))
    }

    return (
        !category ?
            <CategorySelection/>
            :
            <div style={{backgroundColor: '#E4E4E4', width: '99.5vw', overflowX: 'hidden'}}>
                <NavBar/>
                <div style={{margin: '20px 0px 20px 0px', width: '100%', display: 'flex', justifyContent: 'center', minHeight: '100vh'}}>
                    <Grid container spacing={2} style={{width: '100%'}}>
                        <Grid item xs={12} md={12} lg={2}>
                            <ResetCategoryButton/>
                            <FilterPanel onApplyFilter={(filterOptions) => handleApplyFilter(filterOptions)}
                                         onApplySearch={(name) => handleApplySearch(name)}/>
                        </Grid>
                        <Grid item xs={12} md={12} lg={10} style={{textAlign: 'center'}}>
                            <InfiniteScroll
                                pageStart={0}
                                loadMore={handleGetNext}
                                hasMore={!productStore.loadingNext && !!productStore.pagination &&
                                    productStore.pagination.page < productStore.pagination.totalPages}
                                initialLoad={false}
                            >
                                <ProductCatalog products={productStore.products} isLoading={productStore.loading}/>
                            </InfiniteScroll>
                            {productStore.loadingNext &&
                                <div style={{padding: '25px'}}>
                                    <LoadingComponent />
                                </div>
                            }
                        </Grid>
                        <ShoppingCartBadge/>
                    </Grid>
                </div>
                <Footer />
            </div>
    )
}

export default observer(CatalogMain)