import React, { useEffect } from 'react'
import NavbarLayout from '../components/NavbarLayout'
import { useStore } from '../../global/stores/store'
import { observer } from 'mobx-react-lite'

function OnlineShopPanel() {
  const {adminLayoutStore, adminCatalogStore} = useStore()

  useEffect(() => {
    adminCatalogStore.loadProducts()
  }, [])

  return (
    <NavbarLayout>
      {adminLayoutStore.content.body}
    </NavbarLayout>
  )
}

export default observer(OnlineShopPanel)